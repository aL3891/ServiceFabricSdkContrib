using System;
using System.Collections;
using System.Management.Automation;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Client;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.Powershell
{
	[Cmdlet("Publish", "ServiceFabricSolution")]
	public class PublishServiceFabricSolution : PSCmdlet
	{
		[Parameter(ValueFromPipeline = true, Position = 0)]
		public Hashtable AppsHash { get; set; }

		[Parameter]
		public SwitchParameter UseSymlink { get; set; }

		protected override void ProcessRecord()
		{
			dynamic connection = GetVariableValue("ClusterConnection");
			if (connection == null)
				throw new ArgumentNullException("Service fabric connection not found");

			var logger = new PowershellLogger(this);

			var client = ServiceFabricClientFactory.Create(new Uri("http://localhost:19080"));

			
			_ = ExecuteAsync(logger, client);
			logger.Start();
		}

		private async Task ExecuteAsync(PowershellLogger logger, IServiceFabricClient client)
		{
			try
			{
				var apps = new ServiceFabricSolution(AppsHash, SessionState.Path.CurrentFileSystemLocation.Path);
				await client.DeployServiceFabricSolution(apps, UseSymlink.ToBool());
			}
			catch (Exception e)
			{
				logger.LogError(e.Message, e);
			}

			logger.Stop();
		}
	}
}
