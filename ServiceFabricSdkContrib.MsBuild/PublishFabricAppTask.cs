using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using DasMulli.AssemblyInfoGeneration.Sdk;
using Microsoft.Build.Framework;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Client.Http;
using Microsoft.ServiceFabric.Common.Security;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PublishFabricAppTask : ContextAwareTask
	{
		public string PackageLocation { get; set; }
		public string ClusterEndPoint { get; set; }
		public string ThumbPrint { get; set; }
		public string ProjectPath { get; set; }
		public string ParametersFile { get; set; }
		public ITaskItem[] Instances { get; set; }

		protected override bool ExecuteInner()
		{
			if (!string.IsNullOrWhiteSpace(ClusterEndPoint))
			{
				IServiceFabricClient client;
				if (!string.IsNullOrWhiteSpace(ThumbPrint))
				{
					Func<CancellationToken, Task<SecuritySettings>> GetSecurityCredentials = (ct) =>
					{
						var clientCert = new X509Store(StoreName.My, StoreLocation.CurrentUser).Certificates.Find(X509FindType.FindByThumbprint, ThumbPrint, true)[0];
						var remoteSecuritySettings = new RemoteX509SecuritySettings(new List<string> { "server_cert_thumbprint" });
						return Task.FromResult<SecuritySettings>(new X509SecuritySettings(clientCert, remoteSecuritySettings));
					};

					client = new ServiceFabricClientBuilder().UseEndpoints(new Uri(ClusterEndPoint)).UseX509Security(GetSecurityCredentials).BuildAsync().Result;
				}
				else
					client = new ServiceFabricClientBuilder().UseEndpoints(new Uri(ClusterEndPoint)).BuildAsync().Result;
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
