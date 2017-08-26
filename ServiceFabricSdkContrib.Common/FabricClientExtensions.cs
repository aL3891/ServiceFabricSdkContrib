using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors.Query;
using ServiceFabricSdkContrib.MsBuild;

namespace ServiceFabricSdkContrib.Common
{
	public static class FabricClientExtensions
	{
		public static async Task ForeachActor(FabricClient fabricClient, Uri uri, Func<IEnumerable<ActorInformation>, Task> func)
		{
			var partitions = await fabricClient.QueryManager.GetPartitionListAsync(uri);
			await Task.WhenAll(partitions.Select(async partition =>
			{
				var tasks = new List<Task>();
				long key = 0;

				if (partition.PartitionInformation is Int64RangePartitionInformation rp)
					key = rp.LowKey;
				if (partition.PartitionInformation is SingletonPartitionInformation si) //??
					key = 0;
				if (partition.PartitionInformation is NamedPartitionInformation ni)//??
					key = ni.Name.GetHashCode();

				ContinuationToken continuationToken = null;
				var actorServiceProxy = ActorServiceProxy.Create(uri, key);
				do
				{
					var queryResult = await actorServiceProxy.GetActorsAsync(continuationToken, CancellationToken.None);
					tasks.Add(func(queryResult.Items));
					continuationToken = queryResult.ContinuationToken;
				} while (continuationToken != null);
				return tasks;
			}).ToList().SelectMany(a => a.Result));
		}

		public static async Task<GitVersion> SetGitVersion(this ServiceManifestType srv, string BaseDir, string TargetDir, IEnumerable<string> additionalPaths)
		{
			var latest = await Git.GitCommit(BaseDir);
			latest.Diff = await Git.GitDiff(BaseDir);
			var addDiff = new StringBuilder();
			foreach (var item in additionalPaths)
			{
				var v = await Git.GitCommit(item);
				addDiff.Append(await Git.GitDiff(item));
				if (latest.Date < v.Date)
				{
					latest.Date = v.Date;
					latest.Version = v.Version;
				}
			}

			var code = new GitVersion { Version = latest.Version, Date = latest.Date };
			srv.Version = VersionHelper.AppendVersion(srv.Version, latest.Version, VersionHelper.Hash(latest.Diff + addDiff));

			if (srv.ConfigPackage != null)
				foreach (var cv in srv.ConfigPackage)
					cv.Version = await GetPackageVersion(cv.Version, latest, cv.Name, BaseDir);

			if (srv.DataPackage != null)
				foreach (var cv in srv.DataPackage)
					cv.Version = await GetPackageVersion(cv.Version, latest, cv.Name, BaseDir);

			if (srv.CodePackage != null)
			{
				foreach (var cv in srv.CodePackage)
				{
					if (cv.Name == "Code")
					{
						if (latest.Date > code.Date)
						{
							var codeFiles = Directory.GetFileSystemEntries(TargetDir).Where(p => !p.EndsWith("PackageRoot")).Concat(Directory.GetFiles(Path.Combine(BaseDir, "PackageRoot")));
							var vers = await Task.WhenAll(codeFiles.Select(p => Git.GitCommit(p)));
							cv.Version = vers.OrderBy(v => v.Date).First().Version + Git.Hash(string.Join("", codeFiles.Select(cf => Git.GitDiff(cf))) + addDiff);
						}
						else
							cv.Version = srv.Version;
					}
					else
					{
						cv.Version = await GetPackageVersion(cv.Version, latest, cv.Name, BaseDir);
					}
				}
			}
			return new GitVersion { Diff = latest.Diff + addDiff, Date = latest.Date, Version = latest.Version };
		}

		private static async Task<string> GetPackageVersion(string version, GitVersion latest, string name, string BaseDir)
		{
			var path = Path.Combine(BaseDir, "PackageRoot", name);
			var v = await Git.GitCommit(path);

			if (latest.Date < v.Date)
			{
				latest.Date = v.Date;
				latest.Version = v.Version;
			}

			return VersionHelper.AppendVersion(version, v.Version, VersionHelper.Hash(await Git.GitDiff(path)));
		}

		public static ServiceManifestType SetHashVersion(this ServiceManifestType srv, string BaseDir, string TargetDir)
		{
			SHA512Managed sha;
			List<byte> configHash = new List<byte>();
			int offset = 0;

			if (srv.CodePackage != null)
				foreach (var cv in srv.CodePackage)
				{
					sha = new SHA512Managed();
					offset = 0;

					var path = cv.Name == "Code" ? TargetDir : Path.Combine(BaseDir, "PackageRoot", cv.Name);

					foreach (var f in Directory.GetFiles(path, "*.dll").Concat(Directory.GetFiles(path, "*.exe")))
					{
						var b = File.ReadAllBytes(f);
						sha.TransformBlock(b, offset, b.Length, b, offset);
					}

					sha.TransformFinalBlock(new byte[0], offset, 0);
					cv.Version += "." + Uri.EscapeDataString(Convert.ToBase64String(sha.Hash));
					configHash.AddRange(sha.Hash);
				}

			if (srv.ConfigPackage != null)
				foreach (var cv in srv.ConfigPackage)
				{
					sha = new SHA512Managed();
					offset = 0;

					foreach (var f in Directory.GetFiles(Path.Combine(BaseDir, "PackageRoot", cv.Name)))
					{
						var b = File.ReadAllBytes(f);
						sha.TransformBlock(b, offset, b.Length, b, offset);
					}

					sha.TransformFinalBlock(new byte[0], offset, 0);
					cv.Version += "." + Uri.EscapeDataString(Convert.ToBase64String(sha.Hash));
					configHash.AddRange(sha.Hash);
				}

			if (srv.DataPackage != null)
				foreach (var cv in srv.DataPackage)
				{
					sha = new SHA512Managed();
					offset = 0;

					foreach (var f in Directory.GetFiles(Path.Combine(BaseDir, "PackageRoot", cv.Name)))
					{
						var b = File.ReadAllBytes(f);
						sha.TransformBlock(b, offset, b.Length, b, offset);
					}

					sha.TransformFinalBlock(new byte[0], offset, 0);
					cv.Version += "." + Uri.EscapeDataString(Convert.ToBase64String(sha.Hash));
					configHash.AddRange(sha.Hash);
				}

			srv.Version += "." + Uri.EscapeDataString(Convert.ToBase64String(new SHA512Managed().ComputeHash(configHash.ToArray())));
			return srv;
		}

		public static ApplicationManifestType SetGitVersion(this ApplicationManifestType appManifest, IEnumerable<FabricServiceReference> fabricServiceReferences)
		{
			GitVersion version = new GitVersion { Date = DateTime.MinValue, Diff = "" };

			foreach (var spr in fabricServiceReferences)
			{
				var intermediete = Path.Combine(Path.GetDirectoryName(spr.ProjectPath), "obj");
				var commit = File.ReadAllText(Path.Combine(intermediete, "version.txt")).Split(' ');

				var serviceManifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(intermediete, "ServiceManifest.xml"));
				appManifest.ServiceManifestImport
					.First(smi => smi.ServiceManifestRef.ServiceManifestName == spr.ServiceManifestName).ServiceManifestRef
					.ServiceManifestVersion = serviceManifest.Version;

				if (long.TryParse(commit[1], out var d))
				{
					var date = new DateTime(d);
					if (new DateTime(d) > version.Date)
					{
						version.Version = commit[0];
						version.Date = new DateTime(d);
					}
				}

				version.Diff += File.ReadAllText(Path.Combine(intermediete, "diff.txt"));
			}

			appManifest.ApplicationTypeVersion = VersionHelper.AppendVersion(appManifest.ApplicationTypeVersion, version.Version, VersionHelper.Hash(version.Diff));
			return appManifest;
		}
	}
}
