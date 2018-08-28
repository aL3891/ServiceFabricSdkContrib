using System.Collections;
using System.Collections.Generic;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.Common
{
	public class ServiceFabricApplicationSpec
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string PackagePath { get; set; }
		public string ParameterFilePath { get; set; }
		public Dictionary<string, string> Parameters { get; set; }
		public ApplicationManifestType Manifest { get; internal set; }
	}
}
