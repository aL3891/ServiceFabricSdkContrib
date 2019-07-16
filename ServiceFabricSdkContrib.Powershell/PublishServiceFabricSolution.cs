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

		[Parameter(Mandatory = true)]
		public string ClusterEndPoint { get; set; }

		[Parameter]
		public string ThumbPrint { get; set; }

		protected override void ProcessRecord()
		{
			var logger = new PowershellLogger(this);
			_ = ExecuteAsync(logger, ClusterEndPoint, ThumbPrint, SessionState.Path.CurrentFileSystemLocation.Path);
			logger.Start();
		}

		private async Task ExecuteAsync(PowershellLogger logger, string clusterEndPoint, string thumbPrint, string path)
		{
			try
			{
				var client = await new ServiceFabricClientBuilder().ConnectAsync(clusterEndPoint, thumbPrint);
				var apps = new ServiceFabricSolution(AppsHash, path);
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
