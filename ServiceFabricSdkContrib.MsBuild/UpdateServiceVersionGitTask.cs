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
		public string BasePath { get; set; }
		public ITaskItem[] ProjectReferences { get; set; }
		public string Configuration { get; set; }
		public bool CheckIndividualPackages { get; set; }

		public override bool Execute()
		{
			var projectrefs = ProjectReferences?.Select(p => Path.GetDirectoryName(p.GetMetadata("MSBuildSourceProjectFile"))).ToList() ?? new List<string>();
			var baseDir = Path.GetDirectoryName(BasePath);
			var srv = FabricSerializers.ServiceManifestFromFile(Path.Combine(baseDir, "PackageRoot", "ServiceManifest.xml"));
			var ver = srv.SetGitVersion(baseDir, TargetDir, CheckIndividualPackages, projectrefs);
			File.WriteAllText(Path.Combine(baseDir, "pkg", Configuration, "Version.txt"), ver.version + " " + ver.date.Ticks);
			File.WriteAllText(Path.Combine(baseDir, "pkg", Configuration, "Diff.txt"), ver.diff);
			FabricSerializers.SaveServiceManifest(Path.Combine(baseDir, "pkg", Configuration, "ServiceManifest.xml"), srv);
			return true;
		}
	}
}
