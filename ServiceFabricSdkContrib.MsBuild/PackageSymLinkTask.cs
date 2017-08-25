using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Build.Utilities;


namespace ServiceFabricSdkContrib.MsBuild
{
	public class PackageSymLinkTask : Microsoft.Build.Utilities.Task
	{
		public ITaskItem[] IncludeInPackagePaths { get; set; }
		public ITaskItem[] ProjectReferences { get; set; }
		public ITaskItem[] ServiceProjectReferences { get; set; }
		public string Configuration { get; set; }
		public string Platform { get; set; }
		public string ServicePackageRootFolder { get; set; }
		public string ApplicationManifestPath { get; set; }
		public string PackageBehavior { get; set; }
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }
		public string IntermediateOutputPath { get; set; }
		public string BasePath { get; set; }
		public string BaseDir { get; set; }

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

			foreach (var spr in FabricServiceReferenceFactory.Get(ProjectReferences, ServiceProjectReferences))
			{

				string servicePath = Path.Combine(basePath, PackageLocation, spr.ServiceManifestName);

				if (spr.Manifest.ConfigPackage != null)
					foreach (var cv in spr.Manifest.ConfigPackage)
						AddFiles(Path.Combine(spr.ProjectDir, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));

				if (spr.Manifest.DataPackage != null)
					foreach (var cv in spr.Manifest.DataPackage)
						AddFiles(Path.Combine(spr.ProjectDir, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));

				if (spr.Manifest.CodePackage != null)
				{
					foreach (var cv in spr.Manifest.CodePackage)
					{
						if (cv.Name == "Code")
							AddFiles(Path.GetDirectoryName(spr.Targetpath), Path.Combine(servicePath, cv.Name));
						else
							AddFiles(Path.Combine(spr.ProjectDir, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));
					}
				}

				var manifestFile = Path.Combine(spr.ProjectDir, "obj", "ServiceManifest.xml");
				if (File.Exists(manifestFile))
					AddFiles(manifestFile, Path.Combine(servicePath, "ServiceManifest.xml"));
				else
					AddFiles(Path.Combine(spr.ProjectDir, "PackageRoot", "ServiceManifest.xml"), Path.Combine(servicePath, "ServiceManifest.xml"));
			}

			var appmanifestPath = Path.Combine(basePath, PackageLocation, "ApplicationManifest.xml");

			if (File.Exists(Path.Combine(basePath, "obj", "ApplicationManifest.xml")))
				AddFiles(Path.Combine(basePath, "obj", "ApplicationManifest.xml"), appmanifestPath);
			else
				AddFiles(Path.Combine(basePath, "ApplicationPackageRoot", "ApplicationManifest.xml"), appmanifestPath);


			SourceFiles = sourcelist.Select(s => new TaskItem(s)).ToArray();
			DestinationFiles = destlist.Select(s => new TaskItem(s)).ToArray();

			return true;
		}
	}
}