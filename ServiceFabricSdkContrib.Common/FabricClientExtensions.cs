using System;
using System.Collections.Generic;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public static class FabricClientExtensions
	{
		public static async Task<ServiceManifestType> GitVersion(this ServiceManifestType srv, string BaseDir, string TargetDir, IEnumerable<string> additionalPaths)
		{
			var latest = await Git.GitCommit(BaseDir);
			var diff = await Git.GitDiff(BaseDir);
			var addDiff = "";
			foreach (var item in additionalPaths)
			{
				var v = await Git.GitCommit(item);
				addDiff += await Git.GitDiff(item);
				if (latest.Date < v.Date)
				{
					latest.Date = v.Date;
					latest.Version = v.Version;
				}
			}

			var code = new GitVersion { Version = latest.Version, Date = latest.Date };
			srv.Version = latest.Version + Git.Hash(diff + addDiff);

			if (srv.ConfigPackage != null)
				foreach (var cv in srv.ConfigPackage)
					cv.Version = await GetPackageVersion(latest, cv.Name, BaseDir);

			if (srv.DataPackage != null)
				foreach (var cv in srv.DataPackage)
					cv.Version = await GetPackageVersion(latest, cv.Name, BaseDir);

			if (srv.CodePackage != null)
			{
				var codepackage = srv.CodePackage.FirstOrDefault(c => c.Name == "Code");

				if (codepackage != null)
				{
					if (latest.Date > code.Date)
					{
						var codeFiles = Directory.GetFileSystemEntries(TargetDir).Where(p => !p.EndsWith("PackageRoot")).Concat(Directory.GetFiles(Path.Combine(BaseDir, "PackageRoot")));
						var vers = await Task.WhenAll(codeFiles.Select(p => Git.GitCommit(p)));
						codepackage.Version = vers.OrderBy(v => v.Date).First().Version + Git.Hash(string.Join("", codeFiles.Select(cf => Git.GitDiff(cf))) + addDiff);
					}
					else
						codepackage.Version = srv.Version;
				}
			}
			return srv;
		}

		private static async Task<string> GetPackageVersion(GitVersion latest, string name, string BaseDir)
		{
			var path = Path.Combine(BaseDir, "PackageRoot", name);
			var v = await Git.GitCommit(path);

			if (latest.Date < v.Date)
			{
				latest.Date = v.Date;
				latest.Version = v.Version;
			}

			return v.Version + await Git.GitDiffHash(path);
		}

		public static ServiceManifestType ShaHashVersion(this ServiceManifestType srv, string BaseDir, string TargetDir)
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
	}
}
