using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using ServiceFabricSdkContrib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ServiceFabricSdkContrib.MsBuild
{
    public class UpdateApplicationVersionGitTask : Microsoft.Build.Utilities.Task
    {
        public ITaskItem[] ProjectReferences { get; set; }
        public ITaskItem[] ServiceProjectReferences { get; set; }
        public string ApplicationManifestPath { get; set; }
        public string PackageLocation { get; set; }
        public string ProjectPath { get; set; }
		
        public override bool Execute()
        {
            var basePath = Path.GetDirectoryName(ProjectPath);
			
            File.Copy(Path.Combine(basePath, "ApplicationPackageRoot", "ApplicationManifest.xml"), Path.Combine(basePath, PackageLocation, "ApplicationManifest.xml"), true);
            var appManifest = FabricSerializers.AppManifestFromFile(Path.Combine(basePath, "ApplicationPackageRoot", "ApplicationManifest.xml"));

            foreach (var serviceReference in appManifest.ServiceManifestImport)
            {
                var servicePath = Path.Combine(basePath, PackageLocation, serviceReference.ServiceManifestRef.ServiceManifestName, "ServiceManifest.xml");
                if (File.Exists(servicePath))
                {
                    var serviceManifest = FabricSerializers.ServiceManifestFromFile(servicePath);
                    serviceReference.ServiceManifestRef.ServiceManifestVersion = serviceManifest.Version;
                }
            }

            var aggregatedVersion = Uri.EscapeDataString(Convert.ToBase64String(new SHA512Managed().ComputeHash(Encoding.ASCII.GetBytes(string.Join("", appManifest.ServiceManifestImport.Select(ss => ss.ServiceManifestRef.ServiceManifestVersion))))));
            appManifest.ApplicationTypeVersion = appManifest.ApplicationTypeVersion + "." + aggregatedVersion;
            FabricSerializers.SaveAppManifest(Path.Combine(basePath, PackageLocation, "ApplicationManifest.xml"), appManifest);

            return true;
        }

        private IEnumerable<ITaskItem> PatchMetadata(IEnumerable<ITaskItem> projectReferences, IEnumerable<ITaskItem> serviceProjectReferences)
        {
            if (serviceProjectReferences == null)
                serviceProjectReferences = Enumerable.Empty<ITaskItem>();

            if (projectReferences == null)
                projectReferences = Enumerable.Empty<ITaskItem>();

            var res = projectReferences.Where(p => !serviceProjectReferences.Any(spr => spr.ItemSpec == p.ItemSpec)).ToList();

            foreach (var r in res)
            {
                var manifestFile = Path.Combine(Path.GetDirectoryName(r.ItemSpec), "PackageRoot", "ServiceManifest.xml");

                r.SetMetadata("ServiceManifestName", FabricSerializers.ServiceManifestFromFile(manifestFile).Name);
                r.SetMetadata("CodePackageName", "Code");
            }

            return serviceProjectReferences.Concat(res).ToList();
        }

        public Project GetProject(string projectfile)
        {
            return ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == projectfile.ToLower()) ?? new Project(projectfile);
        }
    }	
}

