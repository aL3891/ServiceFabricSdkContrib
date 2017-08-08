using ServiceFabricSdkContrib.Common;
using ServiceFabricSdkContrib.ServiceFabric.ServiceModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Fabric;
using System.Fabric.Description;
using System.Fabric.Health;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.Powershell
{
	[Cmdlet("Publish", "ServiceFabricSolution")]
	public class PublishServiceFabricSolution : PSCmdlet
	{
		[Parameter(ValueFromPipeline = true, Position = 0)]
		public Hashtable AppsHash { get; set; }
		public ServiceFabricApplicationSpec[] Apps { get; set; }

		protected override void ProcessRecord()
		{
			dynamic connection = GetVariableValue("ClusterConnection");
			if (connection == null)
				throw new NullReferenceException();

			Apps = Validate(Parse(AppsHash));
			FabricClient client = connection.FabricClient;
			try
			{
				WriteObject(ProcessAsync(client).Result);
			}
			catch (Exception e)
			{



			}

		}

		public async Task<bool> ProcessAsync(FabricClient client)
		{
			var cluster = FabricSerializers.ClusterManifestFromString(await client.ClusterManager.GetClusterManifestAsync());

			var cn = cluster.FabricSettings.First(s => s.Name == "Management").Parameter.First(s => s.Name == "ImageStoreConnectionString").Value;

			var appTypes = await client.QueryManager.GetApplicationTypeListAsync();
			var appsToUpload = Apps.Where(a => !appTypes.Any(ap => ap.ApplicationTypeName == a.Manifest.ApplicationTypeName && ap.ApplicationTypeVersion == a.Manifest.ApplicationTypeVersion));

			foreach (var item in appsToUpload)
			{
				var n = item.Manifest.ApplicationTypeName + item.Manifest.ApplicationTypeVersion;
				client.ApplicationManager.CopyApplicationPackage(cn, item.PackagePath, n);
				await client.ApplicationManager.ProvisionApplicationAsync(n);
				client.ApplicationManager.RemoveApplicationPackage(cn, n);
			}

			var upgradepolicy = new MonitoredRollingApplicationUpgradePolicyDescription
			{
				MonitoringPolicy = new RollingUpgradeMonitoringPolicy
				{
					FailureAction = UpgradeFailureAction.Rollback,
				},
				UpgradeMode = RollingUpgradeMode.UnmonitoredAuto,
				UpgradeReplicaSetCheckTimeout = TimeSpan.FromSeconds(1)

			};
			List<Task> tasks = new List<Task>();

			foreach (var app in Apps)
			{
				var ap = await client.QueryManager.GetApplicationListAsync(new Uri("fabric:/" + app.Name));

				if (ap.Any())
				{
					if (ap.First().ApplicationTypeName == app.Manifest.ApplicationTypeName && ap.First().ApplicationTypeVersion == app.Manifest.ApplicationTypeVersion)
						continue;

					var ud = new ApplicationUpgradeDescription
					{
						ApplicationName = new Uri("fabric:/" + app.Name),
						TargetApplicationTypeVersion = app.Version,
						UpgradePolicyDescription = upgradepolicy
					};

					foreach (var p in app.Parameters)
						ud.ApplicationParameters[p.Key] = p.Value;

					tasks.Add(client.ApplicationManager.UpgradeApplicationAsync(ud));
				}
				else
				{
					var ad = new ApplicationDescription
					{
						ApplicationName = new Uri("fabric:/" + app.Name),
						ApplicationTypeName = app.Manifest.ApplicationTypeName,
						ApplicationTypeVersion = app.Manifest.ApplicationTypeVersion
					};

					foreach (var p in app.Parameters)
						ad.ApplicationParameters[p.Key] = p.Value;

					tasks.Add(client.ApplicationManager.CreateApplicationAsync(ad));
				}
			}

			await Task.WhenAll(tasks);

			return true;
		}

		public ServiceFabricApplicationSpec[] Validate(ServiceFabricApplicationSpec[] apps)
		{
			foreach (var app in apps)
			{
				if (string.IsNullOrWhiteSpace(app.PackagePath))
					continue;

				if (!Path.IsPathRooted(app.PackagePath))
					app.PackagePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, app.PackagePath);

				app.Manifest = FabricSerializers.AppManifestFromFile(Path.Combine(app.PackagePath, "ApplicationManifest.xml"));
				app.Version = app.Manifest.ApplicationTypeVersion;
			}

			return apps;
		}

		public ServiceFabricApplicationSpec[] Parse(Hashtable appHash)
		{

			return appHash.Keys.OfType<string>().Select(app => new ServiceFabricApplicationSpec
			{
				Name = app,
				Version = ((Hashtable)appHash[app]).ContainsKey("Version") ? ((Hashtable)appHash[app])["Version"].ToString() : null,
				PackagePath = ((Hashtable)appHash[app]).ContainsKey("PackagePath") ? ((Hashtable)appHash[app])["PackagePath"].ToString() : null,
				Parameters = ParseParameters((Hashtable)appHash[app])
			}).ToArray();
		}

		private Dictionary<string, string> ParseParameters(Hashtable hashtable)
		{
			var res = new Dictionary<string, string>();

			if (hashtable.ContainsKey("ParameterFilePath"))
			{
				var pp = hashtable["ParameterFilePath"].ToString();
				if (!Path.IsPathRooted(pp))
					pp = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, pp);

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

