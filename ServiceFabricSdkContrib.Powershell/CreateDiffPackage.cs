using ServiceFabricSdkContrib.Common;
using System;
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
	[Cmdlet("ConvertTo", "ServiceFabricApplicationDiffPackage")]
	public class CreateDiffPackage : PSCmdlet
	{
		[Parameter(ValueFromPipeline = true, Position = 0)]
		public string[] PackagePaths { get; set; }
		
		protected override void ProcessRecord()
		{
			dynamic connection = GetVariableValue("ClusterConnection");
			if (connection == null)
				throw new ArgumentNullException("Service fabric connection not found");

			var client = new ContribFabricClient(connection.FabricClient);

			var tasks = PackagePaths
				.Select(p => string.IsNullOrWhiteSpace(p) ? SessionState.Path.CurrentFileSystemLocation.Path : p)
				.Select(p => Path.IsPathRooted(p) ? p : Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, p))
				.Select(p => client.CreateDiffPackage(p))
				.ToList();

			foreach (var t in tasks)
				WriteObject(t.Result);
		}
	}
}

