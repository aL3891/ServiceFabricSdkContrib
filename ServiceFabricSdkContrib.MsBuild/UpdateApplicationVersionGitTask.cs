using System.IO;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using DasMulli.AssemblyInfoGeneration.Sdk;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class UpdateApplicationVersionGitTask : ContextAwareTask
	{
		public ITaskItem[] ProjectReferences { get; set; }
		public ITaskItem[] ServiceProjectReferences { get; set; }
		public string ApplicationManifestPath { get; set; }
		public string PackageLocation { get; set; }
		public string ProjectPath { get; set; }
		public string Configuration { get; set; }
		public string BaseVersion { get; set; }
		public bool UpdateBaseVersion { get; set; }
		public int MaxHashLength { get; set; }
		public bool SkipHash { get; set; }

		protected override bool ExecuteInner()
		{
			var path = Path.Combine(Path.GetDirectoryName(ProjectPath), "pkg", Configuration, "ApplicationManifest.xml");
			var appManifest = FabricSerializers.AppManifestFromFile(path);
			appManifest.SetGitVersion(UpdateBaseVersion ? BaseVersion : appManifest.ApplicationTypeVersion, FabricServiceReferenceFactory.Get(ProjectReferences, ServiceProjectReferences), Configuration, MaxHashLength, SkipHash);
			FabricSerializers.SaveAppManifest(path, appManifest);
			return true;
		}
	}
}
