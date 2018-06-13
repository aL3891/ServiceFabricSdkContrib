using System;
using System.Collections.Generic;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceFabricSdkContrib.MsBuild;

namespace ServiceFabricSdkContrib.Common
{
	public static class FabricClientExtensions
	{
		public static (string diff, DateTimeOffset date, string version) SetGitVersion(this ServiceManifestType srv, string baseVersion, string baseDir, string targetDir, bool checkPackages, IEnumerable<string> additionalPaths, int maxHashLength, bool skipHash)
		{
			(var latest, string latestFull, var date) = Git.GitCommit(baseDir);
			var diff = skipHash ? "" : Git.GitDiff(baseDir);
			var addDiff = new StringBuilder();
			foreach (var item in additionalPaths)
			{
				(var v, var s, var d) = Git.GitCommit(item, latestFull);
				if (!skipHash)
					addDiff.Append(Git.GitDiff(item));
				if (date < d)
				{
					date = d;
					latest = v;
					latestFull = s;
				}
			}

			DateTimeOffset codeDate = date;
			srv.Version = VersionHelper.AppendVersion(baseVersion, latest, VersionHelper.Hash(diff + addDiff, maxHashLength));
			if (srv.ConfigPackage != null)
				foreach (var cv in srv.ConfigPackage)
					cv.Version = checkPackages ? VersionHelper.AppendVersion(baseVersion, GetPackageVersion(cv.Name, baseDir, ref date, ref latest, ref latestFull, maxHashLength)) : srv.Version;

			if (srv.DataPackage != null)
				foreach (var cv in srv.DataPackage)
					cv.Version = checkPackages ? VersionHelper.AppendVersion(baseVersion, GetPackageVersion(cv.Name, baseDir, ref date, ref latest, ref latestFull, maxHashLength)) : srv.Version;

			if (srv.CodePackage != null)
			{
				foreach (var cv in srv.CodePackage)
				{
					if (cv.Name == "Code" && checkPackages)
					{
						if (date > codeDate)
						{
							var codeFiles = Directory.GetFileSystemEntries(targetDir).Where(p => !p.EndsWith("PackageRoot")).Concat(Directory.GetFiles(Path.Combine(baseDir, "PackageRoot")));

							foreach (var cf in codeFiles)
							{
								(var v, var s, var d) = Git.GitCommit(cf, latestFull);
								if (date < d)
								{
									date = d;
									latest = v;
									latestFull = s;
								}
							}

							cv.Version = VersionHelper.AppendVersion(baseVersion, latest + VersionHelper.Hash(string.Join("", codeFiles.Select(cf => Git.GitDiff(cf))) + addDiff, maxHashLength));
						}
						else
							cv.Version = srv.Version;
					}
					else
					{
						cv.Version = checkPackages ? VersionHelper.AppendVersion(baseVersion, GetPackageVersion(cv.Name, baseDir, ref date, ref latest, ref latestFull, maxHashLength)) : srv.Version;
					}
				}
			}

			return (diff + addDiff, date, latest);
		}

		public static ServiceManifestType SetHashVersion(this ServiceManifestType srv, string baseDir, string targetDir, int maxHashLength)
		{
			SHA512Managed sha;
			List<byte> configHash = new List<byte>();
			int offset = 0;

			if (srv.CodePackage != null)
				foreach (var cv in srv.CodePackage)
				{
					sha = new SHA512Managed();
					offset = 0;

					var path = cv.Name == "Code" ? targetDir : Path.Combine(baseDir, "PackageRoot", cv.Name);

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

					foreach (var f in Directory.GetFiles(Path.Combine(baseDir, "PackageRoot", cv.Name)))
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

					foreach (var f in Directory.GetFiles(Path.Combine(baseDir, "PackageRoot", cv.Name)))
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

		public static ApplicationManifestType SetGitVersion(this ApplicationManifestType appManifest, string baseVersion, IEnumerable<FabricServiceReference> fabricServiceReferences, string configuration, int maxHashLength, bool skipHash)
		{
			DateTime latest = DateTime.MinValue;
			string version = "", diff = "";

			foreach (var spr in fabricServiceReferences)
			{
				var serviceReferencePath = Path.Combine(Path.GetDirectoryName(spr.ProjectPath), "pkg", configuration);
				var versionFile = Path.Combine(serviceReferencePath, "Version.txt");
				var diffFile = Path.Combine(serviceReferencePath, "Diff.txt");

				if (!Directory.Exists(serviceReferencePath) || !File.Exists(versionFile) || !File.Exists(diffFile))
					continue;

				var commit = File.ReadAllText(versionFile).Split(' ');

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

				if (!skipHash)
					diff += File.ReadAllText(diffFile);
			}

			appManifest.ApplicationTypeVersion = VersionHelper.AppendVersion(baseVersion, version, VersionHelper.Hash(diff, maxHashLength));
			return appManifest;
		}

		private static string[] GetPackageVersion(string name, string baseDir, ref DateTimeOffset date, ref string version, ref string latestFull, int maxHashLength)
		{
			var path = Path.Combine(baseDir, "PackageRoot", name);
			var v = Git.GitCommit(path, latestFull);

			if (date < v.date)
			{
				date = v.date;
				version = v.sha;
				latestFull = v.fullSha;
			}

			return new[] { v.sha, VersionHelper.Hash(Git.GitDiff(path), maxHashLength) };
		}
	}
}
