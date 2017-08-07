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


		public static  bool IsSymbolic(string path)
	    {
		    DirectoryInfo pathInfo = new DirectoryInfo(path);
		    return pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
	    }
	}


	public enum SymbolicLink
	{
		File = 0,
		Directory = 1
	}
}
