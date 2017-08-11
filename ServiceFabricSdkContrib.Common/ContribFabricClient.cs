using ServiceFabricSdkContrib.Common;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public class ContribFabricClient
	{

		public ContribFabricClient(FabricClient client)
		{
			Client = client;
		}

		public FabricClient Client { get; }

		public async Task<bool> CreateDiffPackage(string PackagePath)
		{
			var localAppManifest = FabricSerializers.AppManifestFromFile(Path.Combine(PackagePath, "ApplicationManifest.xml"));
			var appTypes = await Client.QueryManager.GetApplicationTypeListAsync();
			var appManifestTasks = appTypes.Where(type => type.ApplicationTypeName == localAppManifest.ApplicationTypeName).Select(type => Client.ApplicationManager.GetApplicationManifestAsync(type.ApplicationTypeName, type.ApplicationTypeVersion));
			await Task.WhenAll(appManifestTasks);
			var serverAppManifests = appManifestTasks.Select(task => FabricSerializers.AppManifestFromString(task.Result)).ToList();

			foreach (var serverAppManifest in serverAppManifests)
			{
				if (serverAppManifest.ApplicationTypeVersion == localAppManifest.ApplicationTypeVersion)
				{
					return false;
				}

				foreach (var serviceImport in serverAppManifest.ServiceManifestImport)
				{
					var localService = localAppManifest.ServiceManifestImport.FirstOrDefault(s => s.ServiceManifestRef.ServiceManifestName == serviceImport.ServiceManifestRef.ServiceManifestName);
					if (localService != null && localService.ServiceManifestRef.ServiceManifestVersion == serviceImport.ServiceManifestRef.ServiceManifestVersion)
						foreach (var dir in Directory.GetDirectories(Path.Combine(PackagePath, serviceImport.ServiceManifestRef.ServiceManifestName)))
							DeleteIfEx(dir);
					else
					{
						var serverServiceManifest = FabricSerializers.ServiceManifestFromString(await Client.ServiceManager.GetServiceManifestAsync(serverAppManifest.ApplicationTypeName, serverAppManifest.ApplicationTypeVersion, serviceImport.ServiceManifestRef.ServiceManifestName));
						var localServiceManifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(PackagePath, serviceImport.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml"));

						if (serverServiceManifest.CodePackage != null && localServiceManifest.CodePackage != null)
							foreach (var package in serverServiceManifest.CodePackage?.Select(sp => localServiceManifest.CodePackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
								DeleteIfEx(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name));

						if (serverServiceManifest.ConfigPackage != null && localServiceManifest.ConfigPackage != null)
							foreach (var package in serverServiceManifest.ConfigPackage?.Select(sp => localServiceManifest.ConfigPackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
								DeleteIfEx(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name));

						if (serverServiceManifest.DataPackage != null && localServiceManifest.DataPackage != null)
							foreach (var package in serverServiceManifest.DataPackage?.Select(sp => localServiceManifest.DataPackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
								DeleteIfEx(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name));
					}
				}
			}

			return true;
		}

		public void DeleteIfEx(string path)
		{
			if (Directory.Exists(path))
				Directory.Delete(path, !Symlink.IsSymbolic(path));
		}
		
		public async Task<bool> DeployServiceFabricSolution(FabricClient client, ServiceFabricSolution Apps)
		{
			var cluster = FabricSerializers.ClusterManifestFromString(await client.ClusterManager.GetClusterManifestAsync());

			var cn = cluster.FabricSettings.First(s => s.Name == "Management").Parameter.First(s => s.Name == "ImageStoreConnectionString").Value;

			var appTypes = await client.QueryManager.GetApplicationTypeListAsync();
			var appsToUpload = Apps.Applications.Where(a => !appTypes.Any(ap => ap.ApplicationTypeName == a.Manifest.ApplicationTypeName && ap.ApplicationTypeVersion == a.Manifest.ApplicationTypeVersion));

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

			foreach (var app in Apps.Applications)
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
