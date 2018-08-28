using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PackageFabricAppTask : Task
	{
		public ITaskItem[] ProjectReferences { get; set; }
		public ITaskItem[] ServiceProjectReferences { get; set; }
		public string Configuration { get; set; }
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }

		[Output]
		public ITaskItem[] SourceFiles { get; set; }
		[Output]
		public ITaskItem[] DestinationFiles { get; set; }

		List<string> sourcelist = new List<string>();
		List<string> destlist = new List<string>();

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
			foreach (var spr in FabricServiceReferenceFactory.Get(ProjectReferences, ServiceProjectReferences))
			{
				string servicePath = Path.Combine(basePath, PackageLocation, spr.ServiceManifestName);
				AddFiles(Path.Combine(spr.ProjectDir, "pkg", Configuration), servicePath);
			}

			AddFiles(Path.Combine(basePath, "ApplicationPackageRoot", "ApplicationManifest.xml"), Path.Combine(basePath, PackageLocation, "ApplicationManifest.xml"));
			SourceFiles = sourcelist.Select(s => new TaskItem(s)).ToArray();
			DestinationFiles = destlist.Select(s => new TaskItem(s)).ToArray();
			return true;
		}
	}
}