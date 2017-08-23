using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public class Git
	{

		public static async Task<GitVersion> GitCommit(string path)
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

		public static async Task<string> GitDiff(string path)
		{
			return await RunGitCommand("diff " + path);
		}

		public static async Task<string> GitDiffHash(string path)
		{
			var v = await GitDiff(path);
			if (v != "")
				return "." + Uri.EscapeDataString(Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(v))));
			else
				return "";
		}

		public static string Hash(string data)
		{
			if (data != "")
				return "." + Uri.EscapeDataString(Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(data))));
			else
				return "";
		}

		private static Task<string> RunGitCommand(string command)
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

	}


	public class GitVersion
	{
		public string Version { get; set; }
		public DateTime Date { get; set; }
		public string Diff { get; set; }
	}
}
