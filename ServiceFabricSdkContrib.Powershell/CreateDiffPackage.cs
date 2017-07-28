using ServiceFabricSdkContrib.Common;
using System;
using System.Fabric;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.Powershell
{
    [Cmdlet("ConvertTo", "ServiceFabricApplicationDiffPackage")]
    public class CreateDiffPackage : PSCmdlet
    {


        [Parameter(ValueFromPipeline = true, Position = 0)]
        public string PackagePath { get; set; }

        protected override void ProcessRecord()
        {
            dynamic connection = GetVariableValue("ClusterConnection");
            FabricClient client = connection.FabricClient;
            if (string.IsNullOrWhiteSpace(PackagePath))
                PackagePath = SessionState.Path.CurrentFileSystemLocation.Path;

            Diff(client).Wait();
        }

        public async Task Diff(FabricClient client)
        {
            var localAppManifest = Helper.FromFile(Path.Combine(PackagePath, "ApplicationManifest.xml"));
            var appTypes = await client.QueryManager.GetApplicationTypeListAsync();
            var appManifestTasks = appTypes.Where(type => type.ApplicationTypeName == localAppManifest.ApplicationTypeName).Select(type => client.ApplicationManager.GetApplicationManifestAsync(type.ApplicationTypeName, type.ApplicationTypeVersion));
            await Task.WhenAll(appManifestTasks);
            var serverAppManifests = appManifestTasks.Select(task => Helper.FromString(task.Result)).ToList();

            foreach (var serverAppManifest in serverAppManifests)
            {
                if (serverAppManifest.ApplicationTypeVersion == localAppManifest.ApplicationTypeVersion)
                    break;

                foreach (var serviceImport in serverAppManifest.ServiceManifestImport)
                {
                    var localService = localAppManifest.ServiceManifestImport.FirstOrDefault(s => s.ServiceManifestRef.ServiceManifestName == serviceImport.ServiceManifestRef.ServiceManifestName);
                    if (localService != null && localService.ServiceManifestRef.ServiceManifestVersion == serviceImport.ServiceManifestRef.ServiceManifestVersion)
                        foreach (var dir in Directory.GetDirectories(Path.Combine(PackagePath, serviceImport.ServiceManifestRef.ServiceManifestName)))
                            Directory.Delete(dir, true);
                    else
                    {
                        var serverServiceManifest = Helper.serviceFromString(await client.ServiceManager.GetServiceManifestAsync(serverAppManifest.ApplicationTypeName, serverAppManifest.ApplicationTypeVersion, serviceImport.ServiceManifestRef.ServiceManifestName));
                        var localServiceManifest = Helper.serviceFromFile(Path.Combine(PackagePath, serviceImport.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml"));

                        foreach (var package in serverServiceManifest.CodePackage.Select(sp => localServiceManifest.CodePackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
                            Directory.Delete(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name), true);

                        foreach (var package in serverServiceManifest.ConfigPackage.Select(sp => localServiceManifest.ConfigPackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
                            Directory.Delete(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name), true);

                        foreach (var package in serverServiceManifest.DataPackage.Select(sp => localServiceManifest.DataPackage.FirstOrDefault(lp => lp.Name == sp.Name && lp.Version == sp.Version)).Where(x => x != null))
                            Directory.Delete(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, package.Name), true);
                    }
                }
            }
        }


    }
}
