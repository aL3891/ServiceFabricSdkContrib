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

			foreach (var spr in PatchMetadata(ProjectReferences, ServiceProjectReferences))
			{
				var serviceProjectPath = Path.GetFullPath(Path.Combine(basePath, spr.Targetpath));
				var project = GetProject(serviceProjectPath);

				var codePath =spr.Targetpath;

				string servicePath = Path.Combine(basePath, PackageLocation, spr.ServiceManifestName);
				if (!Directory.Exists(servicePath))
					Directory.CreateDirectory(servicePath);

				if (!Directory.Exists(Path.Combine(servicePath, spr.CodePackageName)))
					Symlink.CreateSymbolicLink(Path.Combine(servicePath, spr.CodePackageName), codePath, SymbolicLink.Directory);

				if (!Directory.Exists(Path.Combine(servicePath, "Config")))
					Symlink.CreateSymbolicLink(Path.Combine(servicePath, "Config"), Path.Combine(Path.GetDirectoryName(serviceProjectPath), "PackageRoot", "Config"), SymbolicLink.Directory);

				if (!File.Exists(Path.Combine(servicePath, "ServiceManifest.xml")))
				{
					var manifestFile = Path.Combine(Path.GetDirectoryName(serviceProjectPath), project.GetPropertyValue("IntermediateOutputPath"), "ServiceManifest.xml");
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
				if (File.Exists(Path.Combine(IntermediateOutputPath, "ApplicationManifest.xml")))
				{
					Symlink.CreateSymbolicLink(appmanifestPath, Path.Combine(IntermediateOutputPath, "ApplicationManifest.xml"), SymbolicLink.File);
				}
				else
				{
					Symlink.CreateSymbolicLink(appmanifestPath, Path.Combine(basePath, "ApplicationPackageRoot", "ApplicationManifest.xml"), SymbolicLink.File);
				}
			}

			return true;
		}

		private IEnumerable<FabricServiceReference> PatchMetadata(IEnumerable<ITaskItem> projectReferences, IEnumerable<ITaskItem> serviceProjectReferences)
		{
			if (serviceProjectReferences == null)
				serviceProjectReferences = Enumerable.Empty<ITaskItem>();

			if (projectReferences == null)
				projectReferences = Enumerable.Empty<ITaskItem>();

			return ProjectReferences.Select(p => new { p, r = serviceProjectReferences.FirstOrDefault(rr => rr.ItemSpec == p.GetMetadata("OriginalProjectReferenceItemSpec")) }).Select(p => new FabricServiceReference
			{
				Targetpath = p.p.ItemSpec,
				ProjectPath = p.p.GetMetadata("MSBuildSourceProjectFile"),
				Refpath = p.p.GetMetadata("OriginalProjectReferenceItemSpec"),
				ServiceManifestName = p.r.GetMetadata("ServiceManifestName") ?? FabricSerializers.ServiceManifestFromFile(Path.Combine(Path.GetDirectoryName(p.r.ItemSpec), "PackageRoot", "ServiceManifest.xml")).Name,
				CodePackageName = p?.r?.GetMetadata("CodePackageName") ?? "Code"
			});
		}

		public Project GetProject(string projectfile)
		{
			return ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == projectfile.ToLower()) ?? new Project(projectfile);
		}
	}

	public class FabricServiceReference
	{
		public string Targetpath { get; internal set; }
		public string ProjectPath { get; internal set; }
		public string Refpath { get; internal set; }
		public string ServiceManifestName { get; internal set; }
		public string CodePackageName { get; internal set; }
	}

}