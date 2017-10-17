using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System.IO;


namespace ServiceFabricSdkContrib.MsBuild
{
	public class UpdateApplicationVersionGitTask : Microsoft.Build.Utilities.Task
	{
		public ITaskItem[] ProjectReferences { get; set; }
		public ITaskItem[] ServiceProjectReferences { get; set; }
		public string ApplicationManifestPath { get; set; }
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }
		public string Configuration { get; set; }

		public override bool Execute()
		{
			var path = Path.Combine(Path.GetDirectoryName(ProjectPath), "pkg", Configuration, "ApplicationManifest.xml");
			var appManifest = FabricSerializers.AppManifestFromFile(path);
			appManifest.SetGitVersion(FabricServiceReferenceFactory.Get(ProjectReferences, ServiceProjectReferences), Configuration);
			FabricSerializers.SaveAppManifest(path, appManifest);
			return true;
		}
	}
}
