using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public static class Symlink
	{
		[DllImport("kernel32.dll")]
		public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

		public static bool IsSymbolic(string path)
		{
			return new FileInfo(path).Attributes.HasFlag(FileAttributes.ReparsePoint);
		}

		public static void DeleteIfExists(string path)
		{
			if (Directory.Exists(path))
				Directory.Delete(path, !IsSymbolic(path));
		}

		public static string IsUnderSymbolicLink(string path, string basePath)
		{
			var pathToTest = basePath;
			foreach (var segment in path.Split('\\'))
			{
				pathToTest = Path.Combine(pathToTest, segment);
				if (IsSymbolic(pathToTest))
					return pathToTest;
			}
			return null;
		}

		public static void DeletePathWithSymlinks(string path)
		{
			if (IsSymbolic(path))
			{
				Directory.Delete(path, false);
			}
			else
			{
				foreach (var d in Directory.GetDirectories(path))
				{
					DeletePathWithSymlinks(d);
				}
				foreach (var f in Directory.GetFiles(path))
				{
					File.Delete(f);
				}

				if (!Directory.GetDirectories(path).Any())
					Directory.Delete(path, false);
			}
		}
	}


	public enum SymbolicLink
	{
		File = 0,
		Directory = 1
	}
}
