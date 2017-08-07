using ServiceFabricSdkContrib.Common;
using ServiceFabricSdkContrib.ServiceFabric.ServiceModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
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
			WriteObject(ProcessAsync(client).Result);
		}

		public async Task<bool> ProcessAsync(FabricClient client)
		{
			var appTypes = await client.QueryManager.GetApplicationTypeListAsync();
			var appsToUpload = Apps.Select(a => a.PackagePath).Distinct().Where(a => File.Exists(a)).Select(a => new { path = a, manifest = FabricSerializers.AppManifestFromFile(a) }).Where(a => !appTypes.Any(ap => ap.ApplicationTypeName == a.manifest.ApplicationTypeName && ap.ApplicationTypeVersion == a.manifest.ApplicationTypeVersion));

			foreach (var item in appsToUpload)
			{
				var localAppManifest = FabricSerializers.AppManifestFromFile(Path.Combine(item.path, "ApplicationManifest.xml"));
				var n = localAppManifest.ApplicationTypeName + localAppManifest.ApplicationTypeVersion;
				client.ApplicationManager.CopyApplicationPackage("fabric:imageStore", n, n);

				await client.ApplicationManager.ProvisionApplicationAsync(n);
				client.ApplicationManager.RemoveApplicationPackage("fabric:imageStore", n);

			}

			var upgradepolicy = new MonitoredRollingApplicationUpgradePolicyDescription();

			await Task.WhenAll(Apps.Select(a => client.ApplicationManager.UpgradeApplicationAsync(new ApplicationUpgradeDescription
			{
				ApplicationName = new Uri("fabric:/" + a.Name),
				TargetApplicationTypeVersion = a.Version,
				UpgradePolicyDescription = upgradepolicy
			})));

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
				PackagePath = ((Hashtable)appHash[app]).ContainsKey("PackagePath") ? ((Hashtable)appHash[app])["PackagePath"].ToString() : null
			}).ToArray();
		}
	}

	public class ServiceFabricApplicationSpec
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string PackagePath { get; set; }
		public ApplicationManifestType Manifest { get; internal set; }
	}
}

