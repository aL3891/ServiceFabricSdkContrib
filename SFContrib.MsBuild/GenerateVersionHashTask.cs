using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace SfContribTasks
{
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

            var srv = XElement.Load(Path.Combine(Path.GetDirectoryName(BasePath), "PackageRoot", "ServiceManifest.xml"));

            var ver = srv.Elements(srv.Name.Namespace + "CodePackage").FirstOrDefault(c => c.Attribute("Name").Value == "Code")?.Attribute("Version");
            ver.Value = ver.Value + "." + Convert.ToBase64String(sha.Hash);
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


            ver = srv.Elements(srv.Name.Namespace + "ConfigPackage").FirstOrDefault(c => c.Attribute("Name").Value == "Config")?.Attribute("Version");
            ver.Value = ver.Value + "." + Convert.ToBase64String(sha.Hash);


            srv.Attribute("Version").Value = srv.Attribute("Version").Value + Convert.ToBase64String(codeHash.Concat(configHash).ToArray());
            srv.Save(Path.Combine(Path.GetDirectoryName(BasePath), IntermediateOutputPath, "ServiceManifest.xml"));

            return true;
        }
    }
}
