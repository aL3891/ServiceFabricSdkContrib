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

		public static (string diff, DateTimeOffset date, string version) SetGitVersion(this ServiceManifestType srv, string BaseDir, string TargetDir, IEnumerable<string> additionalPaths)
		{
			(var latest, var date) = Git.GitCommit(BaseDir);
			var diff = Git.GitDiff(BaseDir);
			var addDiff = new StringBuilder();
			foreach (var item in additionalPaths)
			{
				(var v, var d) = Git.GitCommit(item);
				addDiff.Append(Git.GitDiff(item));
				if (date < d)
				{
					date = d;
					latest = v;
				}
			}

			DateTimeOffset codeDate = date;
			srv.Version = VersionHelper.AppendVersion(srv.Version, latest, VersionHelper.Hash(diff + addDiff));
			if (srv.ConfigPackage != null)
				foreach (var cv in srv.ConfigPackage)
					cv.Version = VersionHelper.AppendVersion(cv.Version, GetPackageVersion(cv.Name, BaseDir, ref date, ref latest));

			if (srv.DataPackage != null)
				foreach (var cv in srv.DataPackage)
					cv.Version = VersionHelper.AppendVersion(cv.Version, GetPackageVersion(cv.Name, BaseDir, ref date, ref latest));

			if (srv.CodePackage != null)
			{
				foreach (var cv in srv.CodePackage)
				{
					if (cv.Name == "Code")
					{
						if (date > codeDate)
						{
							var codeFiles = Directory.GetFileSystemEntries(TargetDir).Where(p => !p.EndsWith("PackageRoot")).Concat(Directory.GetFiles(Path.Combine(BaseDir, "PackageRoot")));
							var vers = codeFiles.Select(p => Git.GitCommit(p));
							cv.Version = vers.OrderBy(v => v.date).First().sha + VersionHelper.Hash(string.Join("", codeFiles.Select(cf => Git.GitDiff(cf))) + addDiff);
						}
						else
							cv.Version = srv.Version;
					}
					else
					{
						cv.Version = VersionHelper.AppendVersion(cv.Version, GetPackageVersion(cv.Name, BaseDir, ref date, ref latest));
					}
				}
			}
			return (diff + addDiff, date, latest);
		}

		private static string[] GetPackageVersion(string name, string BaseDir, ref DateTimeOffset date, ref string version)
		{
			var path = Path.Combine(BaseDir, "PackageRoot", name);
			var v = Git.GitCommit(path);

			if (date < v.date)
			{
				date = v.date;
				version = v.sha;
			}

			return new[] { v.sha, VersionHelper.Hash(Git.GitDiff(path)) };
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

		public static ApplicationManifestType SetGitVersion(this ApplicationManifestType appManifest, IEnumerable<FabricServiceReference> fabricServiceReferences, string configuration)
		{
			DateTime latest = DateTime.MinValue;
			string version = "", diff = "";

			foreach (var spr in fabricServiceReferences)
			{
				var serviceReferencePath = Path.Combine(Path.GetDirectoryName(spr.ProjectPath), "pkg", configuration);
				if (!Directory.Exists(serviceReferencePath))
					continue;

				var commit = File.ReadAllText(Path.Combine(serviceReferencePath, "version.txt")).Split(' ');

				var serviceManifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(serviceReferencePath, "ServiceManifest.xml"));
				appManifest.ServiceManifestImport
					.First(smi => smi.ServiceManifestRef.ServiceManifestName == spr.ServiceManifestName).ServiceManifestRef
					.ServiceManifestVersion = serviceManifest.Version;

				if (long.TryParse(commit[1], out var d))
				{
					var date = new DateTime(d);
					if (new DateTime(d) > latest)
					{
						version = commit[0];
						latest = new DateTime(d);
					}
				}

				diff += File.ReadAllText(Path.Combine(serviceReferencePath, "diff.txt"));
			}

			appManifest.ApplicationTypeVersion = VersionHelper.AppendVersion(appManifest.ApplicationTypeVersion, version, VersionHelper.Hash(diff));
			return appManifest;
		}
	}
}
