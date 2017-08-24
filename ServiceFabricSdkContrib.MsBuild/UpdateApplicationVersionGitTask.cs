using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System;
using System.Collections.Generic;
using System.Fabric.Management.ServiceModel;
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
			appManifest.SetGitVersion(FabricServiceReferenceFactory.Get(ProjectReferences, ServiceProjectReferences));
			FabricSerializers.SaveAppManifest(Path.Combine(basePath, "obj", "ApplicationManifest.xml"), appManifest);

			return true;
		}
	}
}
