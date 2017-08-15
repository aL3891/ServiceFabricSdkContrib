using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class GenerateVersionHashTask : Microsoft.Build.Utilities.Task
	{
		public string TargetDir { get; set; }
		public string IntermediateOutputPath { get; set; }
		public string BasePath { get; set; }
		public string BaseDir { get; set; }
		public ITaskItem[] ProjectReferences { get; set; }


		public override bool Execute()
		{

			var projectrefs = ProjectReferences?.Select(p => Path.GetDirectoryName(p.GetMetadata("MSBuildSourceProjectFile"))).ToList() ?? new List<string>();

			BaseDir = Path.GetDirectoryName(BasePath);
			var srv = FabricSerializers.ServiceManifestFromFile(Path.Combine(BaseDir, "PackageRoot", "ServiceManifest.xml"));
			srv = srv.GitVersion(BaseDir, TargetDir, projectrefs).Result;
			FabricSerializers.SaveServiceManifest(Path.Combine(BaseDir, IntermediateOutputPath, "ServiceManifest.xml"), srv);
			return true;
		}
	}
}
