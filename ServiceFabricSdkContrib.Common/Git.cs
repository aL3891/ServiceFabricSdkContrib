using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public class Git
	{

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
			using (var repo = new Repository(repoPath))
			{
				var c = repo.Commits.QueryBy(filedir).First();
				return (c.Commit.Sha, c.Commit.Author.When);
			}
		}

		internal static string GitDiff(string baseDir)
		{
			var repoPath = RepoPath(baseDir);
			var filedir = baseDir.Substring(repoPath.Length + 1);
			using (var repo = new Repository(repoPath))
			{
				return string.Join("", repo.Diff.Compare<Patch>().Where(p => p.Path.StartsWith(filedir)).Select(tc => tc.Patch));
			}
		}

	}
}
