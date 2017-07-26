using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.MsBuild
{

    //git log -n 1 --pretty=format:"%h %cI" G:\funrock\rivality-backend\Services.Assets\PackageRoot\


    public class GenerateVersionHashTask : Task
    {

        public string TargetDir { get; set; }
        public string IntermediateOutputPath { get; set; }
        public string BasePath { get; set; }


        public override bool Execute()
        {
            SHA256Managed sha = new SHA256Managed();
            int offset = 0;

            foreach (var f in Directory.GetFiles(TargetDir, "*.dll"))
            {
                var b = File.ReadAllBytes(f);
                sha.TransformBlock(b, offset, b.Length, b, offset);
            }

            sha.TransformFinalBlock(new byte[0], offset, 0);


            XmlSerializer x = new XmlSerializer(typeof(ServiceManifestType));

            var srv = (ServiceManifestType)x.Deserialize(new StreamReader(Path.Combine(Path.GetDirectoryName(BasePath), "PackageRoot", "ServiceManifest.xml")));
            var ver = srv.CodePackage.FirstOrDefault(c => c.Name == "Code");
            ver.Version += ("." + Convert.ToBase64String(sha.Hash));
            var codeHash = sha.Hash;

            sha = new SHA256Managed();
            offset = 0;

            foreach (var f in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(BasePath), "PackageRoot", "Config")))
            {
                var b = File.ReadAllBytes(f);
                sha.TransformBlock(b, offset, b.Length, b, offset);
            }

            sha.TransformFinalBlock(new byte[0], offset, 0);
            var configHash = sha.Hash;


            var codeVer = srv.ConfigPackage.FirstOrDefault(c => c.Name == "Config");
            codeVer.Version += "." + Convert.ToBase64String(sha.Hash);
            
            srv.Version += Convert.ToBase64String(codeHash.Concat(configHash).ToArray());
            x.Serialize(new StreamWriter(Path.Combine(Path.GetDirectoryName(BasePath), IntermediateOutputPath, "ServiceManifest.xml")), srv);

            return true;
        }
    }
}
