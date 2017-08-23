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
	public class UpdateApplicationVersionGitTask : Microsoft.Build.Utilities.Task
	{
		public ITaskItem[] ProjectReferences { get; set; }
		public ITaskItem[] ServiceProjectReferences { get; set; }
		public string ApplicationManifestPath { get; set; }
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }

		public override bool Execute()
		{
			var basePath = Path.GetDirectoryName(ProjectPath);
			var appManifest = FabricSerializers.AppManifestFromFile(Path.Combine(basePath, "ApplicationPackageRoot", "ApplicationManifest.xml"));
			
			GitVersion version = new GitVersion { Date = DateTime.MinValue, Diff = "" };
			foreach (var spr in FabricServiceReference.Get(ProjectReferences, ServiceProjectReferences))
			{
				var intermediete = Path.Combine(Path.GetDirectoryName(spr.ProjectPath), "obj");
				var commit = File.ReadAllText(Path.Combine(intermediete, "version.txt")).Split(' ');

				var serviceManifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(intermediete, "ServiceManifest.xml"));
				appManifest.ServiceManifestImport
					.First(smi => smi.ServiceManifestRef.ServiceManifestName == spr.ServiceManifestName).ServiceManifestRef
					.ServiceManifestVersion = serviceManifest.Version;

				if (DateTime.TryParse(commit[1], out var d) && d > version.Date)
				{
					version.Version = commit[0];
					version.Date = d;
				}

				version.Diff += File.ReadAllText(Path.Combine(intermediete, "diff.txt"));
			}

			if (!string.IsNullOrEmpty(appManifest.ApplicationTypeVersion))
				appManifest.ApplicationTypeVersion += ".";

			appManifest.ApplicationTypeVersion += version.Version;

			if (version.Diff != "")
				appManifest.ApplicationTypeVersion += "." + Uri.EscapeDataString(Convert.ToBase64String(new SHA512Managed().ComputeHash(Encoding.ASCII.GetBytes(version.Diff))));

			FabricSerializers.SaveAppManifest(Path.Combine(basePath, "obj", "ApplicationManifest.xml"), appManifest);

			return true;
		}

		public Project GetProject(string projectfile)
		{
			return ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == projectfile.ToLower()) ?? new Project(projectfile);
		}
	}
}
