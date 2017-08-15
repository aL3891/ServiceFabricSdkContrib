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

		public async Task<bool> CreateDiffPackage(string packagePath)
		{
			var localAppManifest = FabricSerializers.AppManifestFromFile(Path.Combine(packagePath, "ApplicationManifest.xml"));
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
						foreach (var dir in Directory.GetDirectories(Path.Combine(packagePath, serviceImport.ServiceManifestRef.ServiceManifestName)))
							DeleteIfExists(dir);
					else
					{
						var serverServiceManifest = FabricSerializers.ServiceManifestFromString(await Client.ServiceManager.GetServiceManifestAsync(serverAppManifest.ApplicationTypeName, serverAppManifest.ApplicationTypeVersion, serviceImport.ServiceManifestRef.ServiceManifestName));
						var localServiceManifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(packagePath, serviceImport.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml"));

						if (serverServiceManifest.CodePackage != null && localServiceManifest.CodePackage != null)
							foreach (var package in serverServiceManifest.CodePackage?.Select(sp => localServiceManifest.CodePackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
								DeleteIfExists(Path.Combine(packagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name));

						if (serverServiceManifest.ConfigPackage != null && localServiceManifest.ConfigPackage != null)
							foreach (var package in serverServiceManifest.ConfigPackage?.Select(sp => localServiceManifest.ConfigPackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
								DeleteIfExists(Path.Combine(packagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name));

						if (serverServiceManifest.DataPackage != null && localServiceManifest.DataPackage != null)
							foreach (var package in serverServiceManifest.DataPackage?.Select(sp => localServiceManifest.DataPackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
								DeleteIfExists(Path.Combine(packagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name));
					}
				}
			}

			return true;
		}

		private void DeleteIfExists(string path)
		{
			if (Directory.Exists(path))
				Directory.Delete(path, !Symlink.IsSymbolic(path));
		}

		public async Task<bool> DeployServiceFabricSolution(ServiceFabricSolution Apps)
		{
			var cluster = FabricSerializers.ClusterManifestFromString(await Client.ClusterManager.GetClusterManifestAsync());
			var imageStore = cluster.FabricSettings.First(s => s.Name == "Management").Parameter.First(s => s.Name == "ImageStoreConnectionString").Value;

			var appTypes = await Client.QueryManager.GetApplicationTypeListAsync();
			var appsToUpload = Apps.Applications.Where(a => !appTypes.Any(ap => ap.ApplicationTypeName == a.Manifest.ApplicationTypeName && ap.ApplicationTypeVersion == a.Manifest.ApplicationTypeVersion));

			if (Path.IsPathRooted(imageStore))
				await Task.WhenAll(appsToUpload.Select(i => UploadAppToLocalPath(imageStore, i)));
			else
				await Task.WhenAll(appsToUpload.Select(i => UploadApp(imageStore, i)));

			var upgradepolicy = new MonitoredRollingApplicationUpgradePolicyDescription
			{
				MonitoringPolicy = new RollingUpgradeMonitoringPolicy { FailureAction = UpgradeFailureAction.Rollback },
				UpgradeMode = RollingUpgradeMode.UnmonitoredAuto,
				UpgradeReplicaSetCheckTimeout = TimeSpan.FromSeconds(1)
			};

			await Task.WhenAll(Apps.Applications.Select(app => DeployServiceFabricApp(app, upgradepolicy)));

			return true;
		}

		public async Task DeployServiceFabricApp(ServiceFabricApplicationSpec app, UpgradePolicyDescription upgradePolicy)
		{
			var serverAppVersions = await Client.QueryManager.GetApplicationListAsync(new Uri("fabric:/" + app.Name));

			if (serverAppVersions.Any())
			{
				if (serverAppVersions.First().ApplicationTypeName == app.Manifest.ApplicationTypeName && serverAppVersions.First().ApplicationTypeVersion == app.Manifest.ApplicationTypeVersion)
					return;

				var upgradeDescription = new ApplicationUpgradeDescription
				{
					ApplicationName = new Uri("fabric:/" + app.Name),
					TargetApplicationTypeVersion = app.Version,
					UpgradePolicyDescription = upgradePolicy
				};

				foreach (var p in app.Parameters)
					upgradeDescription.ApplicationParameters[p.Key] = p.Value;

				await Client.ApplicationManager.UpgradeApplicationAsync(upgradeDescription);
			}
			else
			{
				var appDescription = new ApplicationDescription
				{
					ApplicationName = new Uri("fabric:/" + app.Name),
					ApplicationTypeName = app.Manifest.ApplicationTypeName,
					ApplicationTypeVersion = app.Manifest.ApplicationTypeVersion
				};

				foreach (var p in app.Parameters)
					appDescription.ApplicationParameters[p.Key] = p.Value;

				await Client.ApplicationManager.CreateApplicationAsync(appDescription);
			}
		}

		private async Task UploadAppToLocalPath(string imageStore, ServiceFabricApplicationSpec app)
		{
			var name = app.Manifest.ApplicationTypeName + app.Manifest.ApplicationTypeVersion;
			Client.ApplicationManager.CopyApplicationPackage(imageStore, app.PackagePath, name);
			await Client.ApplicationManager.ProvisionApplicationAsync(name);
			Client.ApplicationManager.RemoveApplicationPackage(imageStore, name);
		}

		private async Task UploadApp(string imageStore, ServiceFabricApplicationSpec app)
		{
			var name = app.Manifest.ApplicationTypeName + app.Manifest.ApplicationTypeVersion;
			Client.ApplicationManager.CopyApplicationPackage(imageStore, app.PackagePath, name);
			await Client.ApplicationManager.ProvisionApplicationAsync(name);
			Client.ApplicationManager.RemoveApplicationPackage(imageStore, name);
		}
	}
}
