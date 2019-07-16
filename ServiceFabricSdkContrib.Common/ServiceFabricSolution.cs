using System.Collections;
using System.Collections.Generic;
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
			Applications = new List<ServiceFabricApplicationSpec>();
		}

		public void Validate(string basePath)
		{
			foreach (var app in Applications)
			{
				if (string.IsNullOrWhiteSpace(app.PackagePath))
					continue;

				if (!Path.IsPathRooted(app.PackagePath))
					app.PackagePath = Path.Combine(basePath, app.PackagePath);

				if (!Path.IsPathRooted(app.ParameterFilePath))
					app.ParameterFilePath = Path.Combine(basePath, app.ParameterFilePath);

				app.Manifest = FabricSerializers.AppManifestFromFile(Path.Combine(app.PackagePath, "ApplicationManifest.xml"));
				app.Version = app.Manifest.ApplicationTypeVersion;

				var parameters = new Dictionary<string, string>();

				foreach (var p in app.Manifest.Parameters)
				{
					parameters[p.Name] = p.DefaultValue;
				}

				if (!string.IsNullOrWhiteSpace(app.ParameterFilePath))
				{
					var x = XElement.Load(app.ParameterFilePath);
					foreach (var p in x.Element(x.Name.Namespace + "Parameters").Elements(x.Name.Namespace + "Parameter"))
					{
						parameters[p.Attribute("Name").Value] = p.Attribute("Value").Value;
					}
				}

				if (app.Parameters != null)
				{
					foreach (var p in app.Parameters)
					{
						parameters[p.Key] = p.Value;
					}
				}

				app.Parameters = parameters;
			}
		}

		public ServiceFabricSolution(Hashtable appHash, string basePath)
		{
			Applications = appHash.Keys.OfType<string>().Select(app => new ServiceFabricApplicationSpec
			{
				Name = app,
				Version = ((Hashtable)appHash[app]).ContainsKey("Version") ? ((Hashtable)appHash[app])["Version"].ToString() : null,
				PackagePath = ((Hashtable)appHash[app]).ContainsKey("PackagePath") ? ((Hashtable)appHash[app])["PackagePath"].ToString() : null,
				ParameterFilePath = ((Hashtable)appHash[app]).ContainsKey("ParameterFilePath") ? ((Hashtable)appHash[app])["ParameterFilePath"].ToString() : null,
				Parameters = ParseParameters((Hashtable)appHash[app], basePath)
			}).ToList();

			foreach (var app in Applications)
			{
				if (!Path.IsPathRooted(app.PackagePath))
					app.PackagePath = Path.Combine(basePath, app.PackagePath);
				if (!Path.IsPathRooted(app.ParameterFilePath))
					app.ParameterFilePath = Path.Combine(basePath, app.ParameterFilePath);
			}

			Validate(basePath);
		}

		private Dictionary<string, string> ParseParameters(Hashtable hashtable, string basePath)
		{
			var res = new Dictionary<string, string>();
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
}
