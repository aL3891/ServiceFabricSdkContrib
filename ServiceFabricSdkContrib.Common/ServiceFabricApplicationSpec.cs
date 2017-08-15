using ServiceFabricSdkContrib.Common;

using System.Collections;
using System.Collections.Generic;
using System.Fabric.Health;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ServiceFabricSdkContrib.Common
{

	public class ServiceFabricSolution
	{
		public List<ServiceFabricApplicationSpec> Applications { get; set; }

		public ServiceFabricSolution()
		{

		}

		public void Validate(string basePath)
		{
			foreach (var app in Applications)
			{
				if (string.IsNullOrWhiteSpace(app.PackagePath))
					continue;

				if (!Path.IsPathRooted(app.PackagePath))
					app.PackagePath = Path.Combine(basePath, app.PackagePath);

				app.Manifest = FabricSerializers.AppManifestFromFile(Path.Combine(app.PackagePath, "ApplicationManifest.xml"));
				app.Version = app.Manifest.ApplicationTypeVersion;
			}

		}

		public ServiceFabricSolution(Hashtable appHash, string basePath)
		{

			Applications = appHash.Keys.OfType<string>().Select(app => new ServiceFabricApplicationSpec
			{
				Name = app,
				Version = ((Hashtable)appHash[app]).ContainsKey("Version") ? ((Hashtable)appHash[app])["Version"].ToString() : null,
				PackagePath = ((Hashtable)appHash[app]).ContainsKey("PackagePath") ? ((Hashtable)appHash[app])["PackagePath"].ToString() : null,
				Parameters = ParseParameters((Hashtable)appHash[app], basePath)
			}).ToList();

			Validate(basePath);
		}

		private Dictionary<string, string> ParseParameters(Hashtable hashtable, string basePath)
		{
			var res = new Dictionary<string, string>();

			if (hashtable.ContainsKey("ParameterFilePath"))
			{
				var pp = hashtable["ParameterFilePath"].ToString();
				if (!Path.IsPathRooted(pp))
					pp = Path.Combine(basePath, pp);

				var x = XElement.Load(pp);
				foreach (var p in x.Element(x.Name.Namespace + "Parameters").Elements(x.Name.Namespace + "Parameter"))
				{
					res[p.Attribute("Name").Value] = p.Attribute("Value").Value;
				}
			}

			if (hashtable.ContainsKey("Parameters"))
			{
				var inline = (Hashtable)hashtable["Parameters"];
				foreach (var item in inline.Keys)
				{
					res[item.ToString()] = inline[item].ToString();
				}
			}

			return res;
		}
	}

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

