using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using ServiceFabricSdkContrib.ServiceFabric.ServiceModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class GenerateVersionHashTask : Microsoft.Build.Utilities.Task
	{
		public string TargetDir { get; set; }
		public string IntermediateOutputPath { get; set; }
		public string BasePath { get; set; }
		public string BaseDir { get; set; }

		public override bool Execute()
		{
			BaseDir = Path.GetDirectoryName(BasePath);
			var srv = FabricSerializers.ServiceManifestFromFile(Path.Combine(BaseDir, "PackageRoot", "ServiceManifest.xml"));
			srv = GitVersion(srv).Result;
			FabricSerializers.SaveServiceManifest(Path.Combine(BaseDir, IntermediateOutputPath, "ServiceManifest.xml"), srv);
			return true;
		}

		public async Task<GitVersion> GitCommit(string path)
		{
			var res = new GitVersion { };

			var version = (await RunGitCommand("log -n 1 --pretty=format:\"%h %cI\" " + path)).Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
			if (version.Any())
			{
				res.Version = version[0];
				res.Date = DateTime.Parse(version[1]);
			}
			return res;
		}

		public async Task<string> GitDiff(string path)
		{
			return await RunGitCommand("status --porcelain " + path);
		}

		public async Task<string> GitDiffHash(string path)
		{
			var v = await GitDiff(path);
			if (v != "")
				return "."+ Convert.ToBase64String(Encoding.ASCII.GetBytes(v));
			else
				return "";
		}

		public string Hash(string data)
		{
			if (data != "")
				return "." + Convert.ToBase64String(Encoding.ASCII.GetBytes(data));
			else
				return "";
		}

		private Task<string> RunGitCommand(string command)
		{
			var p = Process.Start(new ProcessStartInfo
			{
				FileName = "git",
				Arguments = command,
				CreateNoWindow = true,
				UseShellExecute = false,
				WindowStyle = ProcessWindowStyle.Hidden,
				RedirectStandardOutput = true
			});
			return p.StandardOutput.ReadToEndAsync();
		}



		private async Task<ServiceManifestType> GitVersion(ServiceManifestType srv)
		{
			var latest = await GitCommit(BaseDir);
			var code = new GitVersion { Version = latest.Version, Date = latest.Date };

			srv.Version = latest.Version + await GitDiffHash(BaseDir);

			if (srv.CodePackage != null)
				foreach (var cv in srv.CodePackage.Where(p => p.Name != "Code"))
					cv.Version = await GetPackageVersion(latest, cv.Name);

			if (srv.ConfigPackage != null)
				foreach (var cv in srv.ConfigPackage)
					cv.Version = await GetPackageVersion(latest, cv.Name);

			if (srv.DataPackage != null)
				foreach (var cv in srv.DataPackage)
					cv.Version = await GetPackageVersion(latest, cv.Name);


			var codepackage = srv.CodePackage.FirstOrDefault(c => c.Name == "Code");

			if (codepackage != null)
			{
				if (latest.Date > code.Date)
				{
					var codeFiles = Directory.GetFileSystemEntries(TargetDir).Where(p => !p.EndsWith("PackageRoot")).Concat(Directory.GetFiles(Path.Combine(BaseDir, "PackageRoot")));
					var vers = await Task.WhenAll(codeFiles.Select(p => GitCommit(p)));
					codepackage.Version = vers.OrderBy(v => v.Date).First().Version + Hash(string.Join("", codeFiles.Select(cf => GitDiff(cf))));
				}
				else
					codepackage.Version = srv.Version;
			}
			return srv;
		}

		private async Task<string> GetPackageVersion(GitVersion latest, string name)
		{
			var path = Path.Combine(BaseDir, "PackageRoot", name);
			var v = await GitCommit(path);

			if (latest.Date < v.Date)
			{
				latest.Date = v.Date;
				latest.Version = v.Version;
			}

			return v.Version + await GitDiffHash(path);
		}

		private ServiceManifestType ShaHashVersion(ServiceManifestType srv)
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

	public class GitVersion
	{
		public string Version { get; set; }
		public DateTime Date { get; set; }
	}
}
