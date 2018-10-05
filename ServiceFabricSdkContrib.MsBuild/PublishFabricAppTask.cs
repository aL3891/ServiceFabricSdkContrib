using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Client.Http;
using Microsoft.ServiceFabric.Common.Security;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PublishFabricAppTask : Microsoft.Build.Utilities.Task
	{
		public string PackageLocation { get; set; }
		public string ClusterEndPoint { get; set; }
		public string ThumbPrint { get; set; }
		public string ProjectPath { get; set; }
		public string ParametersFile { get; set; }
		public ITaskItem[] Instances { get; set; }

		public override bool Execute()
		{
			if (!string.IsNullOrWhiteSpace(ClusterEndPoint))
			{
				IServiceFabricClient client;
				if (!string.IsNullOrWhiteSpace(ThumbPrint))
				{
					var cert = new X509Store(StoreName.My, StoreLocation.CurrentUser).Certificates.Find(X509FindType.FindByThumbprint, ThumbPrint, true)[0];
					client = new ServiceFabricHttpClient(new Uri(ClusterEndPoint), new ClientSettings(() => new X509SecuritySettings(cert, new RemoteX509SecuritySettings(new List<string> { }, true))));
				}
				else
					client = new ServiceFabricHttpClient(new Uri(ClusterEndPoint));

				ExecuteAsync(client).Wait();
			}

			return true;
		}

		private async Task ExecuteAsync(IServiceFabricClient client)
		{
			var apps = new ServiceFabricSolution();
			apps.Applications.Add(new ServiceFabricApplicationSpec
			{
				Name = Instances.First().ItemSpec,
				ParameterFilePath = Path.Combine(Path.GetDirectoryName(ProjectPath), ParametersFile),
				Parameters = new Dictionary<string, string> { },
				PackagePath = Path.Combine(Path.GetDirectoryName(ProjectPath), PackageLocation)
			});
			await client.DeployServiceFabricSolution(apps, false);
		}
	}
}
