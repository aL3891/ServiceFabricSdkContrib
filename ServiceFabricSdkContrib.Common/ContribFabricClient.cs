using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Common;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.Common
{
	public class ContribFabricClient
	{
		public ContribFabricClient(IServiceFabricClient client)
		{
			Client = client;
		}

		public ContribFabricClient(IServiceFabricClient client, ILogger logger)
		{
			Client = client;
			Logger = logger;
		}

		public IServiceFabricClient Client { get; }

		public ILogger Logger { get; set; }

		public async Task<bool> CreateDiffPackage(string packagePath)
		{
			var localAppManifest = FabricSerializers.AppManifestFromFile(Path.Combine(packagePath, "ApplicationManifest.xml"));
			var appTypes = await Client.ApplicationTypes.GetApplicationTypeInfoListAsync();

			var appManifestTasks = appTypes.Data.Where(type => type.Name == localAppManifest.ApplicationTypeName).Select(type => Client.ApplicationTypes.GetApplicationManifestAsync(type.Name, type.Version));
			await Task.WhenAll(appManifestTasks);
			var serverAppManifests = appManifestTasks.Select(task => FabricSerializers.AppManifestFromString(task.Result.Manifest)).ToList();
			string pkgPAth = null;

			if (serverAppManifests.Any(serverAppManifest => serverAppManifest.ApplicationTypeVersion == localAppManifest.ApplicationTypeVersion))
			{
				Logger?.LogInfo($"Application {localAppManifest.ApplicationTypeName} {localAppManifest.ApplicationTypeVersion} is already provisioned");
				return false;
			}

			foreach (var serverAppManifest in serverAppManifests)
			{
				foreach (var serviceImport in serverAppManifest.ServiceManifestImport)
				{
					var localService = localAppManifest.ServiceManifestImport.FirstOrDefault(s => s.ServiceManifestRef.ServiceManifestName == serviceImport.ServiceManifestRef.ServiceManifestName);
					if (localService != null && localService.ServiceManifestRef.ServiceManifestVersion == serviceImport.ServiceManifestRef.ServiceManifestVersion)
					{
						Logger?.LogInfo($"Service {localAppManifest.ApplicationTypeName}.{localService.ServiceManifestRef.ServiceManifestName} {localService.ServiceManifestRef.ServiceManifestVersion} is already provisioned");
						foreach (var dir in Directory.GetDirectories(Path.Combine(packagePath, serviceImport.ServiceManifestRef.ServiceManifestName)))
							Symlink.DeleteIfExists(dir);
					}
					else
					{
						var serverServiceManifest = FabricSerializers.ServiceManifestFromString((await Client.ServiceTypes.GetServiceManifestAsync(serverAppManifest.ApplicationTypeName, serverAppManifest.ApplicationTypeVersion, serviceImport.ServiceManifestRef.ServiceManifestName)).Manifest);
						var localServiceManifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(packagePath, serviceImport.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml"));

						// Logger?.LogInfo($"{serverAppManifest.ApplicationTypeName}.{localService.ServiceManifestRef.ServiceManifestName} {localService.ServiceManifestRef.ServiceManifestVersion} not found on server, checking packages");
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

		public async Task<bool> DeployServiceFabricSolution(ServiceFabricSolution apps, bool symlinkProvision)
		{
			var cluster = FabricSerializers.ClusterManifestFromString((await Client.Cluster.GetClusterManifestAsync()).Manifest);
			var appTypes = await Client.ApplicationTypes.GetApplicationTypeInfoListAsync();
			apps.Validate(null);
			var appsToUpload = apps.Applications.Where(a => !appTypes.Data.Any(ap => ap.Name == a.Manifest.ApplicationTypeName && ap.Version == a.Manifest.ApplicationTypeVersion)).ToList();

			if (appsToUpload.Any())
			{
				var imageStore = cluster.FabricSettings.First(s => s.Name == "Management").Parameter.First(s => s.Name == "ImageStoreConnectionString").Value;
				Logger?.LogVerbose($"Using image store {imageStore}");
				var imageStorePath = new Uri(imageStore).LocalPath;

				// if (symlinkProvision && Directory.Exists(imageStorePath))
				// await Task.WhenAll(appsToUpload.Select(i => UploadAppToLocalPath(imageStore, imageStorePath, i)).ToList());
				// else
				await Task.WhenAll(appsToUpload.Select(i => UploadApp(imageStore, i)).ToList());

				Logger?.LogInfo($"Apps uploaded");
			}

			await Task.WhenAll(apps.Applications.Select(app => DeployServiceFabricApp(app)));
			return true;
		}

		public async Task DeployServiceFabricApp(ServiceFabricApplicationSpec app)
		{
			var serverAppVersions = await Client.Applications.GetApplicationInfoListAsync();

			var deployed = serverAppVersions.Data.FirstOrDefault(sa => sa.Name == "fabric:/" + app.Name);

			if (deployed != null)
			{
				if (deployed.TypeVersion == app.Version)
				{
					Logger?.LogInfo($"{app.Name} version {app.Version} is already deployed");
					return;
				}

				var upgradeDescription = new ApplicationUpgradeDescription("fabric:/" + app.Name, app.Version, app.Parameters, UpgradeKind.Rolling, UpgradeMode.UnmonitoredAuto, 1);
				Logger?.LogInfo($"Upgrading app {upgradeDescription.Name} to version {upgradeDescription.TargetApplicationTypeVersion}");
				await Client.Applications.StartApplicationUpgradeAsync(app.Name, upgradeDescription);
			}
			else
			{
				var appDescription = new ApplicationDescription("fabric:/" + app.Name, app.Manifest.ApplicationTypeName, app.Manifest.ApplicationTypeVersion, app.Parameters);

				Logger?.LogInfo($"Creating app {appDescription.Name} with type {appDescription.TypeName} version {appDescription.TypeVersion}");
				Logger?.LogVerbose($"With parameters");
				if (appDescription.Parameters != null)
					foreach (var parameter in appDescription.Parameters.Keys)
						Logger?.LogVerbose($"{parameter} =  {appDescription.Parameters[(string)parameter]}");

				await Client.Applications.CreateApplicationAsync(appDescription);
			}
		}

		private async Task UploadAppToLocalPath(string imageStore, string imageStorep, ServiceFabricApplicationSpec app)
		{
			var name = app.Manifest.ApplicationTypeName + "." + app.Manifest.ApplicationTypeVersion;

			try
			{
				Symlink.CreateSymbolicLink(Path.Combine(imageStorep, name), app.PackagePath, SymbolicLink.Directory);
				await Client.ApplicationTypes.ProvisionApplicationTypeAsync(new ProvisionApplicationTypeDescription(name), 240, CancellationToken.None);
				Symlink.DeleteIfExists(Path.Combine(imageStorep, name));
			}
			catch (FileNotFoundException)
			{
				Symlink.DeleteIfExists(Path.Combine(imageStorep, name));
				await UploadApp(imageStore, app);
			}
		}

		private async Task UploadApp(string imageStore, ServiceFabricApplicationSpec app)
		{
			var name = app.Manifest.ApplicationTypeName + "." + app.Manifest.ApplicationTypeVersion;
			await Client.ImageStore.UploadApplicationPackageAsync(app.PackagePath, true, name);
			await Client.ApplicationTypes.ProvisionApplicationTypeAsync(new ProvisionApplicationTypeDescription(name), 240);
			await Client.ImageStore.DeleteImageStoreContentAsync(name);
		}
	}
}
