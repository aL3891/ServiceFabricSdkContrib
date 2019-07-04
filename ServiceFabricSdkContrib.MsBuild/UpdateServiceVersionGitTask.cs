using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using Microsoft.Build.Utilities;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class UpdateServiceVersionGitTask : Task
	{
		public string TargetDir { get; set; }
		public string BasePath { get; set; }
		public ITaskItem[] ProjectReferences { get; set; }
		public string Configuration { get; set; }
		public bool CheckIndividualPackages { get; set; }
		public string BaseVersion { get; set; }
		public bool UpdateBaseVersion { get; set; }
		public int MaxHashLength { get; set; }
		public bool SkipHash { get; set; }

		public override bool Execute()
		{
			var projectrefs = ProjectReferences?.Select(p => Path.GetDirectoryName(p.GetMetadata("MSBuildSourceProjectFile"))).ToList() ?? new List<string>();
			var baseDir = Path.GetDirectoryName(BasePath);
			var srv = FabricSerializers.ServiceManifestFromFile(Path.Combine(baseDir, "PackageRoot", "ServiceManifest.xml"));
			var ver = srv.SetGitVersion(UpdateBaseVersion ? BaseVersion : srv.Version, baseDir, TargetDir, CheckIndividualPackages, projectrefs, MaxHashLength, SkipHash);
			File.WriteAllText(Path.Combine(baseDir, "pkg", Configuration, "Version.txt"), ver.version + " " + ver.date.Ticks);
			File.WriteAllText(Path.Combine(baseDir, "pkg", Configuration, "Diff.txt"), ver.diff);
			FabricSerializers.SaveServiceManifest(Path.Combine(baseDir, "pkg", Configuration, "ServiceManifest.xml"), srv);
			return true;
		}
	}
}
