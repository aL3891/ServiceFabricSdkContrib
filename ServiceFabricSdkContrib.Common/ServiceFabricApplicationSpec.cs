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
		public ApplicationManifestType Manifest { get; set; }

		public void LoadParameters()
		{
			Manifest = FabricSerializers.AppManifestFromFile(Path.Combine(PackagePath, "ApplicationManifest.xml"));
			Version = Manifest.ApplicationTypeVersion;
			var parameters = new Dictionary<string, string>();

			foreach (var p in Manifest.Parameters)
			{
				parameters[p.Name] = p.DefaultValue;
			}

			if (!string.IsNullOrWhiteSpace(ParameterFilePath))
			{
				var x = XElement.Load(ParameterFilePath);
				foreach (var p in x.Element(x.Name.Namespace + "Parameters").Elements(x.Name.Namespace + "Parameter"))
				{
					parameters[p.Attribute("Name").Value] = p.Attribute("Value").Value;
				}
			}

			if (Parameters != null)
			{
				foreach (var p in parameters.Keys.ToList())
				{
					if (Parameters.ContainsKey(p))
						parameters[p] = Parameters[p];
				}
			}

			Parameters = parameters;
		}
	}
}
