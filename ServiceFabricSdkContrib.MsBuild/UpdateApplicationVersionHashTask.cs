// using Microsoft.Build.Evaluation;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DasMulli.AssemblyInfoGeneration.Sdk;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class UpdateApplicationVersionHashTask : ContextAwareTask
	{
		public ITaskItem[] ProjectReferences { get; set; }
		public ITaskItem[] ServiceProjectReferences { get; set; }
		public string ApplicationManifestPath { get; set; }
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }
		public string Configuration { get; set; }
		public int MaxHashLength { get; set; }

		protected override bool ExecuteInner()
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

			var aggregatedVersion = VersionHelper.Hash(string.Join("", appManifest.ServiceManifestImport.Select(ss => ss.ServiceManifestRef.ServiceManifestVersion)), MaxHashLength);
			appManifest.ApplicationTypeVersion = appManifest.ApplicationTypeVersion + "." + aggregatedVersion;
			FabricSerializers.SaveAppManifest(path, appManifest);

			return true;
		}

		// public Project GetProject(string projectfile)
		// {
		// return ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == projectfile.ToLower()) ?? new Project(projectfile);
		// }
	}
}
