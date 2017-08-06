using ServiceFabricSdkContrib.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.Fabric.Management.ServiceModel;
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
        public string[] PackagePath { get; set; }

        [Parameter(ValueFromPipeline = true, Position = 0)]
        public Hashtable apps { get; set; }

        protected override void ProcessRecord()
        {

            foreach (var apa in apps.OfType<DictionaryEntry>())
            {
                WriteDebug(apa.Key.ToString() + " " + apa.Value);
            }

            return;

            dynamic connection = GetVariableValue("ClusterConnection");
            if (connection == null)
                throw new NullReferenceException();


            FabricClient client = connection.FabricClient;



            for (int i = 0; i < PackagePath.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(PackagePath[i]))
                    PackagePath[i] = SessionState.Path.CurrentFileSystemLocation.Path;

                if (!Path.IsPathRooted(PackagePath[i]))
                    PackagePath[i] = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, PackagePath[i]);
            }

            WriteObject(Doit(client).Result);
        }

        public async Task<bool> Doit(FabricClient client)
        {

            var upgradepolicy = new MonitoredRollingApplicationUpgradePolicyDescription();

            foreach (var p in PackagePath)
            {
                var localAppManifest = FabricSerializers.AppManifestFromFile(Path.Combine(p, "ApplicationManifest.xml"));
                var n = localAppManifest.ApplicationTypeName + localAppManifest.ApplicationTypeVersion;
                client.ApplicationManager.CopyApplicationPackage("fabric:imageStore", n, n);

                await client.ApplicationManager.ProvisionApplicationAsync(n);
                client.ApplicationManager.RemoveApplicationPackage("fabric:imageStore", n);
            }


            await client.ApplicationManager.UpgradeApplicationAsync(new ApplicationUpgradeDescription
            {
                ApplicationName = new Uri(""),
                TargetApplicationTypeVersion = "",
                UpgradePolicyDescription = upgradepolicy
            });
            return true;
        }
    }
}

