using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

// using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class CleanServiceFabricApplicationTask : Microsoft.Build.Utilities.Task
	{
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }
		public ITaskItem[] CleanItems { get; set; }
		[Output]
		public ITaskItem[] SymlinkItems { get; set; }
		private HashSet<string> symPaths = new HashSet<string>();

		public override bool Execute()
		{
			BasePath = Path.GetDirectoryName(ProjectPath);
			SymlinkItems = CleanItems.Where(c => IsUnderSymbolicLink(c.ItemSpec)).ToArray();
			return true;
		}

		public string BasePath { get; set; }

		public bool IsUnderSymbolicLink(string path)
		{
			if (path.Contains("*"))
				return false;

			var fullPath = Path.Combine(BasePath, path);
			if (symPaths.Any(s => path.StartsWith(fullPath)))
				return true;

			var symPath = Symlink.IsUnderSymbolicLink(path, BasePath);

			if (symPath != null)
			{
				symPaths.Add(symPath);
				return true;
			}
			else
				return false;
		}
	}
}