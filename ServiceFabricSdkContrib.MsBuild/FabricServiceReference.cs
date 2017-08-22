using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class FabricServiceReference
	{
		public string Targetpath { get; internal set; }
		public string ProjectPath { get; internal set; }
		public string Refpath { get; internal set; }
		public string ServiceManifestName { get; internal set; }
		public string CodePackageName { get; internal set; }


		public static IEnumerable<FabricServiceReference> Get(IEnumerable<ITaskItem> projectReferences, IEnumerable<ITaskItem> serviceProjectReferences)
		{
			if (serviceProjectReferences == null)
				serviceProjectReferences = Enumerable.Empty<ITaskItem>();

			if (projectReferences == null)
				projectReferences = Enumerable.Empty<ITaskItem>();

			return projectReferences
				.Select(p => new
				{
					projectReference = p,
					servicereference = serviceProjectReferences.FirstOrDefault(rr => rr.ItemSpec == p.GetMetadata("OriginalProjectReferenceItemSpec"))
				})
				.Select(refs => new FabricServiceReference
				{
					Targetpath = refs.projectReference.ItemSpec,
					ProjectPath = refs.projectReference.GetMetadata("MSBuildSourceProjectFile"),
					ServiceManifestName = refs.servicereference?.GetMetadata("ServiceManifestName") ?? FabricSerializers.ServiceManifestFromFile(Path.Combine(Path.GetDirectoryName(refs.projectReference.GetMetadata("MSBuildSourceProjectFile")), "PackageRoot", "ServiceManifest.xml")).Name,
					CodePackageName = refs.servicereference?.GetMetadata("CodePackageName") ?? "Code"
				});
		}
	}
}