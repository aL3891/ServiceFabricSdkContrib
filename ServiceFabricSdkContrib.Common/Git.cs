using LibGit2Sharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public class Git
	{
		static ConcurrentDictionary<string, Repository> repos = new ConcurrentDictionary<string, Repository>();
		static ConcurrentDictionary<string, Commit> logs = new ConcurrentDictionary<string, Commit>();
		static ConcurrentDictionary<string, string> head = new ConcurrentDictionary<string, string>();

		public static string RepoPath(string path)
		{
			if (Directory.Exists(Path.Combine(path, ".git")))
				return path;
			else if (path.TrimEnd('\\').Where(c => c == '\\').Count() == 1)
				throw new ArgumentException();
			else
				return RepoPath(Path.GetDirectoryName(path));
		}

		public static (string sha, DateTimeOffset date) GitCommit(string baseDir)
		{
			var repoPath = RepoPath(baseDir);
			var filedir = baseDir.Substring(repoPath.Length + 1);
			var repo = repos.GetOrAdd(repoPath, r => new Repository(repoPath));

			if (repo.Head.Tip.Sha != head.GetOrAdd(repoPath, r => repo.Head.Tip.Sha))
			{
				logs.Clear();
			}

			Commit c = null;
			c = logs.GetOrAdd(filedir, f =>
			{
				try
				{
					var res = repo.Head.Commits.Take(150).Where(cc => repo.Diff.Compare<TreeChanges>(cc.Parents.First().Tree, cc.Tree).Any(tc => tc.OldPath.Contains(filedir))).FirstOrDefault();
					if (res == null)
						res = repo.Lookup<Commit>(GitExeCommit(baseDir, repoPath).Result);

					return res;
				}
				catch (KeyNotFoundException)
				{
					return null;
				}
			});

			if (c != null)
				return (GetShortSha(repo, c.Id.Sha, 7), c.Author.When);
			else
				return ("", DateTimeOffset.MinValue);

		}


		public static async Task<string> GitExeCommit(string path, string repoPath)
		{
			return await RunGitCommand("log -n 1 --pretty=format:\"%h\" " + path, repoPath);
		}

		private static Task<string> RunGitCommand(string command, string repoPath)
		{
			var p = Process.Start(new ProcessStartInfo
			{
				FileName = "git",
				Arguments = command,
				CreateNoWindow = true,
				UseShellExecute = false,
				WindowStyle = ProcessWindowStyle.Hidden,
				RedirectStandardOutput = true,
				WorkingDirectory = repoPath
			});
			return p.StandardOutput.ReadToEndAsync();
		}

		public static string GetShortSha(Repository repo, string sha, int length)
		{
			try
			{
				var shortSha = sha.Substring(0, length);
				var c = repo.Lookup<Commit>(shortSha);
				return shortSha;
			}
			catch (AmbiguousSpecificationException e)
			{

				if (sha.Length >= length + 1)
					GetShortSha(repo, sha, length + 1);
				else
					throw;

			}
			throw new ArgumentException();
		}

		internal static string GitDiff(string baseDir)
		{
			return "";
			var repoPath = RepoPath(baseDir);
			var filedir = baseDir.Substring(repoPath.Length + 1);
			var repo = repos.GetOrAdd(repoPath, r => new Repository(repoPath));
			return string.Join("", repo.Diff.Compare<Patch>().Where(p => p.Path.StartsWith(filedir)).Select(tc => tc.Patch));
		}
	}
}
