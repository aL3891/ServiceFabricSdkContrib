using System;
using System.Fabric;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SFContrib.Powershell
{
    [Cmdlet("Create", "DiffPackage")]
    public class CreateDiffPackage : PSCmdlet
    {
        static XmlSerializer serializer = new XmlSerializer(typeof(ApplicationManifestType));
        static XmlSerializer x = new XmlSerializer(typeof(ServiceManifestType));

        public string PackagePath { get; set; }

        protected override void ProcessRecord()
        {
            dynamic connection = GetVariableValue("ClusterConnection");
            FabricClient client = connection.FabricClient;
            Diff(client).Wait();
        }

        public async Task Diff(FabricClient client)
        {
            var localAppManifest = FromFile(Path.Combine(PackagePath, "ApplicationManifest.xml"));
            var appTypes = await client.QueryManager.GetApplicationTypeListAsync();
            var appManifestTasks = appTypes.Where(type => type.ApplicationTypeName == localAppManifest.ApplicationTypeName).Select(type => client.ApplicationManager.GetApplicationManifestAsync(type.ApplicationTypeName, type.ApplicationTypeVersion));
            await Task.WhenAll(appManifestTasks);
            var serverAppManifests = appManifestTasks.Select(task => FromString(task.Result)).ToList();

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
                        var serv = await client.ServiceManager.GetServiceManifestAsync(serverAppManifest.ApplicationTypeName, serverAppManifest.ApplicationTypeVersion, serviceImport.ServiceManifestRef.ServiceManifestName);
                        var servman = serviceFromString(serv);

                        var localServiceman = serviceFromFile(Path.Combine(PackagePath, serviceImport.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml"));

                        foreach (var apa in servman.CodePackage.Select(ss => localServiceman.CodePackage.FirstOrDefault(ll => ll.Name == ss.Name && ll.Version == ss.Version)).Where(x => x != null))
                            Directory.Delete(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, apa.Name), true);

                        foreach (var apa in servman.ConfigPackage.Select(ss => localServiceman.ConfigPackage.FirstOrDefault(ll => ll.Name == ss.Name && ll.Version == ss.Version)).Where(x => x != null))
                            Directory.Delete(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, apa.Name), true);

                        foreach (var apa in servman.DataPackage.Select(ss => localServiceman.DataPackage.FirstOrDefault(ll => ll.Name == ss.Name && ll.Version == ss.Version)).Where(x => x != null))
                            Directory.Delete(Path.Combine(PackagePath, localService.ServiceManifestRef.ServiceManifestName, apa.Name), true);
                    }
                }
            }
        }

        private ApplicationManifestType FromFile(string path)
        {
            using (var stream = new StreamReader(path))
                return (ApplicationManifestType)serializer.Deserialize(stream);
        }

        private static ApplicationManifestType FromString(string g)
        {
            using (var stream = new StringReader(g))
                return (ApplicationManifestType)serializer.Deserialize(stream);
        }

        private static ServiceManifestType serviceFromFile(string g)
        {
            using (var stream = new StringReader(g))
                return (ServiceManifestType)serializer.Deserialize(stream);
        }

        private static ServiceManifestType serviceFromString(string g)
        {
            using (var stream = new StringReader(g))
                return (ServiceManifestType)x.Deserialize(stream);
        }
    }
}
