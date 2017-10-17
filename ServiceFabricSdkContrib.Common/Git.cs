using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public class Git
	{
		public static (string sha, DateTimeOffset date) GitCommit(string baseDir)
		{
			using (var repo = new Repository(baseDir))
			{
				return (repo.Head.Tip.Sha, repo.Head.Tip.Author.When);
			}
		}

		internal static string GitDiff(string baseDir)
		{
			using (var repo = new Repository(baseDir))
			{
				return string.Join("", repo.Diff.Compare<Patch>().Select(tc => tc.Patch));
			}
		}

	}
}
