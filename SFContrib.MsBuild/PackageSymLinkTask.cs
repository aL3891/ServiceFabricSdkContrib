using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SfContribTasks
{
    public class PackageSymLinkTask : Microsoft.Build.Utilities.Task
    {

        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        public ITaskItem[] ProjectReferences { get; set; }
        public ITaskItem[] ServiceProjectReferences { get; set; }
        public string Configuration { get; set; }
        public string Platform { get; set; }
        public string ServicePackageRootFolder { get; set; }
        public string ApplicationManifestPath { get; set; }
        public string PackageBehavior { get; set; }
        public string PackageLocation { get; set; }
        public string BasePath { get; set; }

        public ITaskItem[] IncludeInPackagePaths { get; set; }

        public override bool Execute()
        {
            var bp = Path.GetDirectoryName(BasePath);

            foreach (var spr in ServiceProjectReferences)
            {
                var prp = Path.GetFullPath(Path.Combine(bp, spr.ItemSpec));
                var p = GetProject(prp);

                var path = p.GetPropertyValue("TargetDir");

                string servicePath = Path.Combine(bp, PackageLocation, spr.GetMetadata("ServiceManifestName"));
                if (!Directory.Exists(servicePath))
                    Directory.CreateDirectory(servicePath);

                if (!Directory.Exists(Path.Combine(servicePath, spr.GetMetadata("CodePackageName"))))
                    CreateSymbolicLink(Path.Combine(servicePath, spr.GetMetadata("CodePackageName")), path, SymbolicLink.Directory);

                if (!Directory.Exists(Path.Combine(servicePath, "Config")))
                    CreateSymbolicLink(Path.Combine(servicePath, "Config"), Path.Combine(Path.GetDirectoryName(prp), "PackageRoot", "Config"), SymbolicLink.Directory);

                var manifestFile = Path.Combine(Path.GetDirectoryName(prp), p.GetPropertyValue("IntermediateOutputPath"), "ServiceManifest.xml");
                if (!File.Exists(manifestFile))
                    manifestFile = Path.Combine(Path.GetDirectoryName(prp), "PackageRoot", "ServiceManifest.xml");

                File.Copy(manifestFile, Path.Combine(servicePath, "ServiceManifest.xml"));

            }

            File.Copy(Path.Combine(bp, "ApplicationPackageRoot", "ApplicationManifest.xml"), Path.Combine(bp, PackageLocation, "ApplicationManifest.xml"));
            XElement app = XElement.Load(Path.Combine(bp, "ApplicationPackageRoot", "ApplicationManifest.xml"));

            foreach (var sr in app.Element("ServiceManifestImport").Elements("ServiceManifestRef"))
            {
                var srv = XElement.Load(Path.Combine(bp, PackageLocation, sr.Attribute("ServiceManifestName").Value, "ServiceManifest.xml"));
                sr.Attribute("ServiceManifestVersion").Value = srv.Attribute("Version").Value;
            }

            var aggregatedVersion = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Join("", app.Element("ServiceManifestImport").Elements("ServiceManifestRef").Select(smr => smr.Attribute("ServiceManifestVersion").Value))));
            app.Attribute("ApplicationTypeVersion").Value = app.Attribute("ApplicationTypeVersion").Value + "." + aggregatedVersion;

            app.Save(Path.Combine(bp, PackageLocation, "ApplicationManifest.xml"));

            return true;
        }

        public Project GetProject(string projectfile)
        {
            var apa = ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == projectfile.ToLower());

            if (apa == null)
                apa = new Project(projectfile);

            return apa;
        }
    }

    enum SymbolicLink
    {
        File = 0,
        Directory = 1
    }
}

