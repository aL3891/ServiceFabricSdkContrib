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
				return (GetShortSha(repo,c.Commit.Id.Sha,7), c.Commit.Author.When);
			}
		}

		public static string GetShortSha(Repository repo , string sha, int length)
		{
			try
			{
				var shortSha = sha.Substring(0, length);
				var c = repo.Lookup<Commit>(shortSha);
				return shortSha;
			}
			catch (AmbiguousSpecificationException e)
			{

				if (sha.Length >= length+1)
					GetShortSha(repo,sha,length+1);
				else
					throw;
					
			}
			throw new ArgumentException();
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
