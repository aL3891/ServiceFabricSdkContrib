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

			Apps = Parse(AppsHash);
			Apps = Fixup(Apps);
			FabricClient client = connection.FabricClient;
			WriteObject(Doit(client).Result);
		}

		public async Task<bool> Doit(FabricClient client)
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

			await client.ApplicationManager.UpgradeApplicationAsync(new ApplicationUpgradeDescription
			{
				ApplicationName = new Uri(""),
				TargetApplicationTypeVersion = "",
				UpgradePolicyDescription = upgradepolicy
			});
			return true;
		}

		public ServiceFabricApplicationSpec[] Fixup(ServiceFabricApplicationSpec[] apps)
		{
			foreach (var item in apps)
			{
				if (string.IsNullOrWhiteSpace(item.PackagePath))
					continue; //					item.PackagePath = SessionState.Path.CurrentFileSystemLocation.Path;

				if (!Path.IsPathRooted(item.PackagePath))
					item.PackagePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, item.PackagePath);

				item.Manifest = FabricSerializers.AppManifestFromFile(item.PackagePath);
			}


			return apps;
		}

		public ServiceFabricApplicationSpec[] Parse(Hashtable apps)
		{

			return new ServiceFabricApplicationSpec[0];
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

