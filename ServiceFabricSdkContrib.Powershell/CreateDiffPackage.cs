using ServiceFabricSdkContrib.Common;
using System;
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
	[Cmdlet("ConvertTo", "ServiceFabricApplicationDiffPackage")]
	public class CreateDiffPackage : PSCmdlet
	{
		[Parameter(ValueFromPipeline = true, Position = 0)]
		public string PackagePath { get; set; }


		protected override void ProcessRecord()
		{
			dynamic connection = GetVariableValue("ClusterConnection");
			FabricClient client = connection.FabricClient;

			var cc = new ContribFabricClient(client);

			if (string.IsNullOrWhiteSpace(PackagePath))
				PackagePath = SessionState.Path.CurrentFileSystemLocation.Path;


			if (!Path.IsPathRooted(PackagePath))
				PackagePath = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, PackagePath);


			WriteObject(cc.CreateDiffPackage(PackagePath));
		}
	}
}

