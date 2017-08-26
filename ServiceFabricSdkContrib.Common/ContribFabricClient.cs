using ServiceFabricSdkContrib.Common;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Common
{
	public class ContribFabricClient
	{
		public ContribFabricClient(FabricClient client)
		{
			Client = client;
		}

		public ContribFabricClient(FabricClient client, ILogger logger)
		{
			Client = client;
			Logger = logger;
		}

		public FabricClient Client { get; }

		public ILogger Logger { get; set; }

		public async Task<bool> CreateDiffPackage(string packagePath)
		{
			var localAppManifest = FabricSerializers.AppManifestFromFile(Path.Combine(packagePath, "ApplicationManifest.xml"));
			var appTypes = await Client.QueryManager.GetApplicationTypeListAsync();
			var appManifestTasks = appTypes.Where(type => type.ApplicationTypeName == localAppManifest.ApplicationTypeName).Select(type => Client.ApplicationManager.GetApplicationManifestAsync(type.ApplicationTypeName, type.ApplicationTypeVersion));
			await Task.WhenAll(appManifestTasks);
			var serverAppManifests = appManifestTasks.Select(task => FabricSerializers.AppManifestFromString(task.Result)).ToList();
			string pkgPAth = null;

			if (serverAppManifests.Any(serverAppManifest => serverAppManifest.ApplicationTypeVersion == localAppManifest.ApplicationTypeVersion))
			{
				Logger?.LogInfo($"Application {localAppManifest.ApplicationTypeName} {localAppManifest.ApplicationTypeVersion} already is provisioned");
				return false;
			}

			foreach (var serverAppManifest in serverAppManifests)
			{
				foreach (var serviceImport in serverAppManifest.ServiceManifestImport)
				{
					var localService = localAppManifest.ServiceManifestImport.FirstOrDefault(s => s.ServiceManifestRef.ServiceManifestName == serviceImport.ServiceManifestRef.ServiceManifestName);
					if (localService != null && localService.ServiceManifestRef.ServiceManifestVersion == serviceImport.ServiceManifestRef.ServiceManifestVersion)
					{
						Logger?.LogVerbose($"Service {localService.ServiceManifestRef.ServiceManifestName} {localService.ServiceManifestRef.ServiceManifestVersion} is already provisioned");
						foreach (var dir in Directory.GetDirectories(Path.Combine(packagePath, serviceImport.ServiceManifestRef.ServiceManifestName)))
							Symlink.DeleteIfExists(dir);
					}
					else
					{
						var serverServiceManifest = FabricSerializers.ServiceManifestFromString(await Client.ServiceManager.GetServiceManifestAsync(serverAppManifest.ApplicationTypeName, serverAppManifest.ApplicationTypeVersion, serviceImport.ServiceManifestRef.ServiceManifestName));
						var localServiceManifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(packagePath, serviceImport.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml"));
						//Logger?.LogInfo($"{serverAppManifest.ApplicationTypeName}.{localService.ServiceManifestRef.ServiceManifestName} {localService.ServiceManifestRef.ServiceManifestVersion} not found on server, checking packages");

						if (serverServiceManifest.CodePackage != null && localServiceManifest.CodePackage != null)
							foreach (var package in serverServiceManifest.CodePackage.Where(sp => localServiceManifest.CodePackage.Any(lp => lp.Name == sp.Name && lp.Version == sp.Version)))
							{
								pkgPAth = Path.Combine(packagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name);
								if (Directory.Exists(pkgPAth))
								{
									Logger?.LogInfo($"Deleting {localAppManifest.ApplicationTypeName}.{localService.ServiceManifestRef.ServiceManifestName}.{package.Name} {package.Version}");
									Symlink.DeleteIfExists(pkgPAth);
								}
							}

						if (serverServiceManifest.ConfigPackage != null && localServiceManifest.ConfigPackage != null)
							foreach (var package in serverServiceManifest.ConfigPackage.Where(sp => localServiceManifest.ConfigPackage.Any(lp => lp.Name == sp.Name && lp.Version == sp.Version)))
							{
								pkgPAth = Path.Combine(packagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name);
								if (Directory.Exists(pkgPAth))
								{
									Logger?.LogInfo($"Deleting {localAppManifest.ApplicationTypeName}.{localService.ServiceManifestRef.ServiceManifestName}.{package.Name} {package.Version}");
									Symlink.DeleteIfExists(pkgPAth);
								}
							}

						if (serverServiceManifest.DataPackage != null && localServiceManifest.DataPackage != null)
							foreach (var package in serverServiceManifest.DataPackage.Where(sp => localServiceManifest.DataPackage.Any(lp => lp.Name == sp.Name && lp.Version == sp.Version)))
							{
								pkgPAth = Path.Combine(packagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name);
								if (Directory.Exists(pkgPAth))
								{
									Logger?.LogInfo($"Deleting {localAppManifest.ApplicationTypeName}.{localService.ServiceManifestRef.ServiceManifestName}.{package.Name} {package.Version}");
									Symlink.DeleteIfExists(pkgPAth);
								}
							}
					}
				}
			}
			return true;
		}

		public async Task<bool> DeployServiceFabricSolution(ServiceFabricSolution Apps)
		{
			var cluster = FabricSerializers.ClusterManifestFromString(await Client.ClusterManager.GetClusterManifestAsync());
			var appTypes = await Client.QueryManager.GetApplicationTypeListAsync();
			var appsToUpload = Apps.Applications.Where(a => !appTypes.Any(ap => ap.ApplicationTypeName == a.Manifest.ApplicationTypeName && ap.ApplicationTypeVersion == a.Manifest.ApplicationTypeVersion)).ToList();

			if (appsToUpload.Any())
			{
				var imageStore = cluster.FabricSettings.First(s => s.Name == "Management").Parameter.First(s => s.Name == "ImageStoreConnectionString").Value;
				Logger?.LogVerbose($"Using image store {imageStore}");
				var imageStorePath = new Uri(imageStore).LocalPath;

				if (Directory.Exists(imageStorePath))
					await Task.WhenAll(appsToUpload.Select(i => UploadAppToLocalPath(imageStorePath, i)).ToList());
				else
					await Task.WhenAll(appsToUpload.Select(i => UploadApp(imageStore, i)).ToList());

				Logger?.LogInfo($"Apps uploaded");
			}

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
				if (serverAppVersions.Any(s => s.ApplicationTypeName == app.Manifest.ApplicationTypeName && s.ApplicationTypeVersion == app.Manifest.ApplicationTypeVersion))
				{
					Logger?.LogInfo($"{app.Name} version {app.Version} is already deployed");
					return;
				}

				var upgradeDescription = new ApplicationUpgradeDescription
				{
					ApplicationName = new Uri("fabric:/" + app.Name),
					TargetApplicationTypeVersion = app.Version,
					UpgradePolicyDescription = upgradePolicy
				};

				foreach (var p in app.Parameters)
					upgradeDescription.ApplicationParameters[p.Key] = p.Value;

				Logger?.LogInfo($"Upgrading app {upgradeDescription.ApplicationName} to version {upgradeDescription.TargetApplicationTypeVersion}");
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

				Logger?.LogInfo($"Creating app {appDescription.ApplicationName} with type {appDescription.ApplicationTypeName} version {appDescription.ApplicationTypeVersion}");
				Logger?.LogVerbose($"With parameters");
				if (appDescription.ApplicationParameters != null)
					foreach (var parameter in appDescription.ApplicationParameters.Keys)
						Logger?.LogVerbose($"{parameter} =  {appDescription.ApplicationParameters[(string)parameter]}");

				await Client.ApplicationManager.CreateApplicationAsync(appDescription);
			}
		}

		private async Task UploadAppToLocalPath(string imageStore, ServiceFabricApplicationSpec app)
		{
			var name = app.Manifest.ApplicationTypeName + app.Manifest.ApplicationTypeVersion;
			Symlink.CreateSymbolicLink(Path.Combine(imageStore, name), app.PackagePath, SymbolicLink.Directory);
			await Client.ApplicationManager.ProvisionApplicationAsync(name, TimeSpan.FromHours(1), CancellationToken.None);
			Symlink.DeleteIfExists(Path.Combine(imageStore, name));
		}

		private async Task UploadApp(string imageStore, ServiceFabricApplicationSpec app)
		{
			var name = app.Manifest.ApplicationTypeName + app.Manifest.ApplicationTypeVersion;
			await Task.Run(() => Client.ApplicationManager.CopyApplicationPackage(imageStore, app.PackagePath, name, TimeSpan.FromHours(1)));
			await Client.ApplicationManager.ProvisionApplicationAsync(name, TimeSpan.FromHours(1), CancellationToken.None);
			Client.ApplicationManager.RemoveApplicationPackage(imageStore, name);
		}
	}
}
