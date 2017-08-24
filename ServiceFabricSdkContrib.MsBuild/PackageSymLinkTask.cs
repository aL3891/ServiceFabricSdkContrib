using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;


namespace ServiceFabricSdkContrib.MsBuild
{
	public class PackageSymLinkTask : Microsoft.Build.Utilities.Task
	{
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

		public ITaskItem[] IncludeInPackagePaths { get; set; }

		public override bool Execute()
		{
			var basePath = Path.GetDirectoryName(ProjectPath);

			foreach (var spr in FabricServiceReferenceFactory.Get(ProjectReferences, ServiceProjectReferences))
			{
				var serviceProjectPath = spr.ProjectPath;
				var codePath = Path.GetDirectoryName(spr.Targetpath);

				string servicePath = Path.Combine(basePath, PackageLocation, spr.ServiceManifestName);
				if (!Directory.Exists(servicePath))
					Directory.CreateDirectory(servicePath);

				if (!Directory.Exists(Path.Combine(servicePath, spr.CodePackageName)))
					Symlink.CreateSymbolicLink(Path.Combine(servicePath, spr.CodePackageName), codePath, SymbolicLink.Directory);

				if (!Directory.Exists(Path.Combine(servicePath, "Config")))
					Symlink.CreateSymbolicLink(Path.Combine(servicePath, "Config"), Path.Combine(Path.GetDirectoryName(serviceProjectPath), "PackageRoot", "Config"), SymbolicLink.Directory);

				if (!File.Exists(Path.Combine(servicePath, "ServiceManifest.xml")))
				{
					var manifestFile = Path.Combine(Path.GetDirectoryName(serviceProjectPath), "obj", "ServiceManifest.xml");
					if (File.Exists(manifestFile))
					{
						Symlink.CreateSymbolicLink(Path.Combine(servicePath, "ServiceManifest.xml"), manifestFile, SymbolicLink.File);
					}
					else
					{
						manifestFile = Path.Combine(Path.GetDirectoryName(serviceProjectPath), "PackageRoot", "ServiceManifest.xml");
						File.Copy(manifestFile, Path.Combine(servicePath, "ServiceManifest.xml"));
					}
				}
			}

			var appmanifestPath = Path.Combine(basePath, PackageLocation, "ApplicationManifest.xml");

			if (!File.Exists(appmanifestPath))
			{
				if (File.Exists(Path.Combine(basePath, "obj", "ApplicationManifest.xml")))
				{
					Symlink.CreateSymbolicLink(appmanifestPath, Path.Combine(basePath, "obj", "ApplicationManifest.xml"), SymbolicLink.File);
				}
				else
				{
					Symlink.CreateSymbolicLink(appmanifestPath, Path.Combine(basePath, "ApplicationPackageRoot", "ApplicationManifest.xml"), SymbolicLink.File);
				}
			}

			return true;
		}

		public Project GetProject(string projectfile)
		{
			return ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == projectfile.ToLower()) ?? new Project(projectfile);
		}
	}
}