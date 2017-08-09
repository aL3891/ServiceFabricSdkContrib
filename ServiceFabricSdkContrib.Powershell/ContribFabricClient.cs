using ServiceFabricSdkContrib.Common;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Powershell
{
    public class ContribFabricClient
    {

		public ContribFabricClient(FabricClient client)
		{
			Client = client;
		}

		public FabricClient Client { get; }

		public async Task<bool> CreateDiffPackage(string PackagePath,string basePath)
		{
			if (!Path.IsPathRooted(PackagePath))
				PackagePath = Path.Combine(basePath, PackagePath);

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

	}
}
