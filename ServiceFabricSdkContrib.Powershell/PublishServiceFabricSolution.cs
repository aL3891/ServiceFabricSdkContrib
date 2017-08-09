using ServiceFabricSdkContrib.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Fabric;
using System.Fabric.Description;
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

		protected override void ProcessRecord()
		{
			dynamic connection = GetVariableValue("ClusterConnection");
			if (connection == null)
				throw new NullReferenceException();

			var apps = ServiceFabricApplicationSpec.Validate(ServiceFabricApplicationSpec.Parse(AppsHash, SessionState.Path.CurrentFileSystemLocation.Path), SessionState.Path.CurrentFileSystemLocation.Path);
			FabricClient client = connection.FabricClient;

			try
			{
				WriteObject(DeployServiceFabricSolution(client, apps).Result);
			}
			catch (AggregateException e)
			{
				WriteError(new ErrorRecord(e.InnerExceptions.First(), "", ErrorCategory.InvalidData, null));
			}
		}

		public async Task<bool> DeployServiceFabricSolution(FabricClient client, ServiceFabricApplicationSpec[] Apps)
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
	}
}

