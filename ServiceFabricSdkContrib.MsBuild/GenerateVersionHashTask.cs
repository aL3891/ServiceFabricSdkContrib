using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Fabric.Management.ServiceModel;
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
			var srv = Helper.serviceFromFile(Path.Combine(BaseDir, "PackageRoot", "ServiceManifest.xml"));
			srv = GitVersion(srv).Result;
			Helper.SaveService(Path.Combine(BaseDir, IntermediateOutputPath, "ServiceManifest.xml"), srv);
			return true;
		}

		public async Task<DateTime> GetGitDate(string path)
		{
			var version = (await RunGitCommand("log -n 1 --pretty=format:\"%cI\" " + path));

			if (!string.IsNullOrWhiteSpace(version))
				return DateTime.Parse(version);
			else
				return DateTime.MinValue;
		}

		public async Task<(string, DateTime)> GetCommit(string path)
		{
			var version = (await RunGitCommand("log -n 1 --pretty=format:\"%h\" " + path)).Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

			var commit = version[0];
			var date = DateTime.Parse(version[1]);

			return (commit, date);
		}

		public async Task<(string, DateTime)> GetGitVersion(string path)
		{
			var version = (await RunGitCommand("log -n 1 --pretty=format:\"%h %cI\" " + path)).Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

			var commit = version[0];
			var date = DateTime.Parse(version[1]);

			return (commit, date);
		}

		public async Task<string> GetGitDiffHash(string path)
		{
			var changes = await RunGitCommand("status --porcelain " + path);

			if (changes != "")
			{
				changes = await RunGitCommand("status -v -v " + path);
				var date = DateTime.UtcNow;
				changes = Convert.ToBase64String(Encoding.ASCII.GetBytes(changes));
				return changes;
			}
			return "";
		}

		public async Task<(string, DateTime)> GetGitVersionWithHash(string path)
		{
			var res = await GetGitVersion(path);

			var changes = await RunGitCommand("status --porcelain " + path);

			if (changes != "")
			{
				changes = await RunGitCommand("status -v -v " + path);
				var date = DateTime.UtcNow;
				changes = Convert.ToBase64String(Encoding.ASCII.GetBytes(changes));
				return (res.Item1 + "." + changes, res.Item2);
			}
			return res;
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
			srv.Version = (await GetGitVersion(BaseDir)).Item1;
			var latest = (version: "", date: DateTime.MinValue);

			if (srv.CodePackage != null)
				foreach (var cv in srv.CodePackage)
				{
					var path = cv.Name == "Code" ? BaseDir : Path.Combine(BaseDir, "PackageRoot", cv.Name);
					(var v, var date) = await GetGitVersionWithHash(Path.Combine(BaseDir, "PackageRoot", cv.Name));
					cv.Version = v;

					if (latest.date < date)
						latest = (v, date);
				}

			if (srv.ConfigPackage != null)
				foreach (var cv in srv.ConfigPackage)
				{
					(var v, var date) = await GetGitVersionWithHash(Path.Combine(BaseDir, "PackageRoot", cv.Name));
					cv.Version = v;

					if (latest.date < date)
						latest = (v, date);
				}

			if (srv.DataPackage != null)
				foreach (var cv in srv.DataPackage)
				{
					(var v, var date) = await GetGitVersionWithHash(Path.Combine(BaseDir, "PackageRoot", cv.Name));
					cv.Version = v;

					if (latest.date < date)
						latest = (v, date);
				}

			if (srv.CodePackage.FirstOrDefault(c => c.Name == "Code").Version != latest.version)
			{
				var vers = await Task.WhenAll(Directory.GetFileSystemEntries(TargetDir).Where(p => !p.EndsWith("PackageRoot")).Concat(Directory.GetFiles(Path.Combine(BaseDir, "PackageRoot", "ServiceManifest.xml"))).Select(p => GetGitVersion(p)));

				srv.CodePackage.FirstOrDefault(c => c.Name == "Code").Version = vers.OrderBy(v => v.Item2).First().Item1 + GetGitDiffHash(BaseDir);
			}

			return srv;
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
}
