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

		public void Validate()
		{
			foreach (var app in Applications)
			{
				app.LoadParameters();
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

			Validate();
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
