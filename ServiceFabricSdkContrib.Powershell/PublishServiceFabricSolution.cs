using ServiceFabricSdkContrib.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Fabric;
using System.Fabric.Description;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.Powershell
{
	[Cmdlet("Publish", "ServiceFabricSolution")]
	public class PublishServiceFabricSolution : PSCmdlet
	{
		[Parameter(ValueFromPipeline = true, Position = 0)]
		public Hashtable AppsHash { get; set; }

		protected override void ProcessRecord()
		{
			dynamic connection = GetVariableValue("ClusterConnection");
			if (connection == null)
				throw new NullReferenceException();

			var apps = new ServiceFabricSolution(AppsHash, SessionState.Path.CurrentFileSystemLocation.Path);
			FabricClient client = connection.FabricClient;
			var cc = new ContribFabricClient(client);
			
			try
			{
				WriteObject(cc.DeployServiceFabricSolution(client, apps).Result);
			}
			catch (AggregateException e)
			{
				WriteError(new ErrorRecord(e.InnerExceptions.First(), "", ErrorCategory.InvalidData, null));
			}
		}

	}
}

