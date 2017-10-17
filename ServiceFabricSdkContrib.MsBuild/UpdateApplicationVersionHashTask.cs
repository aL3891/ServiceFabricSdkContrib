using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace ServiceFabricSdkContrib.MsBuild
{
	public class UpdateApplicationVersionHashTask : Microsoft.Build.Utilities.Task
	{
		public ITaskItem[] ProjectReferences { get; set; }
		public ITaskItem[] ServiceProjectReferences { get; set; }
		public string ApplicationManifestPath { get; set; }
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }
		public string Configuration { get; set; }

		public override bool Execute()
		{
			var basePath = Path.GetDirectoryName(ProjectPath);
			var path = Path.Combine(basePath, "pkg", Configuration, "ApplicationManifest.xml");
			var appManifest = FabricSerializers.AppManifestFromFile(path);

			foreach (var serviceReference in appManifest.ServiceManifestImport)
			{
				var servicePath = Path.Combine(basePath, PackageLocation, serviceReference.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml");
				if (File.Exists(servicePath))
				{
					var serviceManifest = FabricSerializers.ServiceManifestFromFile(servicePath);
					serviceReference.ServiceManifestRef.ServiceManifestVersion = serviceManifest.Version;
				}
			}

			var aggregatedVersion = VersionHelper.Hash(string.Join("", appManifest.ServiceManifestImport.Select(ss => ss.ServiceManifestRef.ServiceManifestVersion)));
			appManifest.ApplicationTypeVersion = appManifest.ApplicationTypeVersion + "." + aggregatedVersion;
			FabricSerializers.SaveAppManifest(path, appManifest);

			return true;
		}

		public Project GetProject(string projectfile)
		{
			return ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == projectfile.ToLower()) ?? new Project(projectfile);
		}
	}
}

