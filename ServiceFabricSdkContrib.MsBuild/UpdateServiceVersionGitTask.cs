using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class UpdateServiceVersionGitTask : Microsoft.Build.Utilities.Task
	{
		public string TargetDir { get; set; }
		public string IntermediateOutputPath { get; set; }
		public string BasePath { get; set; }
		public string BaseDir { get; set; }
		public ITaskItem[] ProjectReferences { get; set; }
		public string Configuration { get; set; }

		public override bool Execute()
		{
			var projectrefs = ProjectReferences?.Select(p => Path.GetDirectoryName(p.GetMetadata("MSBuildSourceProjectFile"))).ToList() ?? new List<string>();
			BaseDir = Path.GetDirectoryName(BasePath);
			var srv = FabricSerializers.ServiceManifestFromFile(Path.Combine(BaseDir, "PackageRoot", "ServiceManifest.xml"));
			var ver = srv.SetGitVersion(BaseDir, TargetDir, projectrefs);
			File.WriteAllText(Path.Combine(BaseDir, "pkg", Configuration, "version.txt"), ver.version + " " + ver.date.Ticks);
			File.WriteAllText(Path.Combine(BaseDir, "pkg", Configuration, "diff.txt"), ver.diff);
			FabricSerializers.SaveServiceManifest(Path.Combine(BaseDir, "pkg", Configuration, "ServiceManifest.xml"), srv);
			return true;
		}
	}
}
