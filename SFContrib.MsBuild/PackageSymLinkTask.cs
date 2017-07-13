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
            Console.WriteLine("woopwoop");


            var ProjectFile = ProjectReferences.First().ItemSpec;

            ProjectFile = Path.GetFullPath(ProjectFile);


            var apa = ProjectCollection.GlobalProjectCollection.LoadedProjects.FirstOrDefault(p => p.FullPath.ToLower() == ProjectFile.ToLower());

            if (apa == null)
                apa = new Project(ProjectFile);


            var path = apa.GetPropertyValue("TargetDir");


            return true;
        }
    }

    enum SymbolicLink
    {
        File = 0,
        Directory = 1
    }
}

