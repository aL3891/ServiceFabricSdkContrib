using System.Collections.Generic;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class FabricServiceReference
	{
		public string Targetpath { get;  set; }
		public string ProjectPath { get;  set; }
		public string Refpath { get;  set; }
		public string ServiceManifestName { get;  set; }
		public string CodePackageName { get;  set; }
		public ServiceManifestType Manifest { get; set; }
		public string ProjectDir { get; set; }
	}
}