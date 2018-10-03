using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Client;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PublishFabricAppTask : Microsoft.Build.Utilities.Task
	{
		public string PackageLocation { get; set; }
		public string ClusterEndPoint { get; set; }
		public string ThumbPrint { get; set; }

		public override bool Execute()
		{
			var client = ServiceFabricClientFactory.Create(new Uri("http://localhost:19080"));
			ExecuteAsync(client).Wait();
			return true;
		}

		private async Task ExecuteAsync(IServiceFabricClient client)
		{
			var apps = new ServiceFabricSolution();
			apps.Applications.Add(new ServiceFabricApplicationSpec { PackagePath = PackageLocation });
			await client.DeployServiceFabricSolution(apps, false);
		}
	}
}
