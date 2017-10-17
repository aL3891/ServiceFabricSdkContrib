using Microsoft.Build.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Utilities;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PackageFabricServiceTask : Task
	{
		public string Configuration { get; set; }	
		public string ProjectPath { get; set; }
		public string TargetDir { get; set; }

		[Output]
		public ITaskItem[] SourceFiles { get; set; }
		[Output]
		public ITaskItem[] DestinationFiles { get; set; }

		List<string> sourcelist = new List<string>(), destlist = new List<string>();

		public void AddFiles(string source, string destination)
		{
			FileAttributes attr = File.GetAttributes(source);

			if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
			{
				foreach (var d in Directory.GetFileSystemEntries(source))
				{
					AddFiles(d, Path.Combine(destination, d.Substring(source.Length).TrimStart('\\')));
				}
			}
			else
			{
				sourcelist.Add(source);
				destlist.Add(destination);
			}
		}

		public override bool Execute()
		{
			var basePath = Path.GetDirectoryName(ProjectPath);
			var manifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(basePath, "PackageRoot", "ServiceManifest.xml"));
			string servicePath = Path.Combine(basePath, "pkg", Configuration);

			if (manifest.ConfigPackage != null)
				foreach (var cv in manifest.ConfigPackage)
					AddFiles(Path.Combine(basePath, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));

			if (manifest.DataPackage != null)
				foreach (var cv in manifest.DataPackage)
					AddFiles(Path.Combine(basePath, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));

			if (manifest.CodePackage != null)
			{
				foreach (var cv in manifest.CodePackage)
				{
					if (cv.Name == "Code")
						AddFiles(TargetDir, Path.Combine(servicePath, cv.Name));
					else
						AddFiles(Path.Combine(basePath, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));
				}
			}

			AddFiles(Path.Combine(basePath, "PackageRoot", "ServiceManifest.xml"), Path.Combine(servicePath, "ServiceManifest.xml"));
			SourceFiles = sourcelist.Select(s => new TaskItem(s)).ToArray();
			DestinationFiles = destlist.Select(s => new TaskItem(s)).ToArray();
			return true;
		}
	}
}