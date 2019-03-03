using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using ServiceFabricSdkContrib.Common;
using DasMulli.AssemblyInfoGeneration.Sdk;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PackageFabricServiceTask : ContextAwareTask
	{
		List<string> sourcelist = new List<string>();
		List<string> destlist = new List<string>();

		public string Configuration { get; set; }
		public string ProjectDir { get; set; }
		public string PublishDir { get; set; }

		[Output]
		public ITaskItem[] SourceFiles { get; set; }
		[Output]
		public ITaskItem[] DestinationFiles { get; set; }

		public void AddFiles(string source, string destination)
		{
			FileAttributes attr = File.GetAttributes(source);

			if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
			{
				foreach (var d in Directory.GetFileSystemEntries(source))
				{
					AddFiles(d, Path.Combine(destination, d.Substring(source.Length).TrimStart('\\')));
				}
			}
			else
			{
				sourcelist.Add(source);
				destlist.Add(destination);
			}
		}

		protected override bool ExecuteInner()
		{
			var basePath = ProjectDir;
			var manifest = FabricSerializers.ServiceManifestFromFile(Path.Combine(basePath, "PackageRoot", "ServiceManifest.xml"));
			string servicePath = Path.Combine(basePath, "pkg", Configuration);

			if (manifest.ConfigPackage != null)
				foreach (var cv in manifest.ConfigPackage)
					AddFiles(Path.Combine(basePath, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));

			if (manifest.DataPackage != null)
				foreach (var cv in manifest.DataPackage)
					AddFiles(Path.Combine(basePath, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));

			if (manifest.CodePackage != null)
			{
				foreach (var cv in manifest.CodePackage)
				{
					if (cv.Name == "Code")
						AddFiles(Path.Combine(basePath, PublishDir), Path.Combine(servicePath, cv.Name));
					else
						AddFiles(Path.Combine(basePath, "PackageRoot", cv.Name), Path.Combine(servicePath, cv.Name));
				}
			}

			AddFiles(Path.Combine(basePath, "PackageRoot", "ServiceManifest.xml"), Path.Combine(servicePath, "ServiceManifest.xml"));
			SourceFiles = sourcelist.Select(s => new TaskItem(s)).ToArray();
			DestinationFiles = destlist.Select(s => new TaskItem(s)).ToArray();
			return true;
		}
	}
}