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
		static ConcurrentDictionary<string, (string, string, DateTimeOffset)> logs = new ConcurrentDictionary<string, (string, string, DateTimeOffset)>();
		static ConcurrentDictionary<string, string> head = new ConcurrentDictionary<string, string>();

		public static string RepoPath(string path)
		{
			if (Directory.Exists(Path.Combine(path, ".git")) || File.Exists(Path.Combine(path, ".git")))
				return path;
			else if (path.TrimEnd('\\').Where(c => c == '\\').Count() == 1)
				throw new ArgumentException();
			else
				return RepoPath(Path.GetDirectoryName(path));
		}

		public static (string sha, string fullSha, DateTimeOffset date) GitCommit(string baseDir, string latestSha = "")
		{
			var repoPath = RepoPath(baseDir);

			string filedir = "";
			if (baseDir.Length > repoPath.Length)
				filedir = baseDir.Substring(repoPath.Length + 1);

			var repo = repos.GetOrAdd(repoPath, r => new Repository(repoPath));

			if (repo.Head.Tip.Sha != head.GetOrAdd(repoPath, r => repo.Head.Tip.Sha))
			{
				logs.Clear();
			}

			return logs.GetOrAdd(filedir, f =>
			{
				Commit commit = null;
				//cache latest checked commit and the latest changed commit at that time and use that to limit the lookup

				IEnumerable<Commit> commits = repo.Head.Commits.Take(1000);
				if (latestSha != "")
					commits = commits.TakeWhile(c => c.Parents.Any(p => p.Sha != latestSha));

				commit = commits.Where(cc=> cc.Parents.Any()).Where(cc => repo.Diff.Compare<TreeChanges>(cc.Parents.First().Tree, cc.Tree).Any(tc => tc.OldPath.Contains(filedir))).FirstOrDefault();

				if (commit == null && latestSha == "")
					commit = repo.Lookup<Commit>(GitExeCommit(baseDir).Result);

				if (commit != null)
					return (GetShortSha(repo, commit.Id.Sha, 7), commit.Id.Sha, commit.Author.When);
				else
					return ("", "", DateTimeOffset.MinValue);
			});
		}


		public static async Task<string> GitExeCommit(string path)
		{
			return await RunGitCommand("log -n 1 --pretty=format:%H " + path);
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
			var repoPath = RepoPath(baseDir);
			string filedir = "";
			if (baseDir.Length > repoPath.Length)
				filedir = baseDir.Substring(repoPath.Length + 1);
			var repo = repos.GetOrAdd(repoPath, r => new Repository(repoPath));
			return string.Join("", repo.Diff.Compare<Patch>().Where(p => p.Path.StartsWith(filedir)).Select(tc => tc.Patch));
		}
	}
}
