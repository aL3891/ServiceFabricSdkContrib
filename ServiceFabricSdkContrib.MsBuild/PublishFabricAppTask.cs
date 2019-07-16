using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Client.Http;
using Microsoft.ServiceFabric.Common.Security;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PublishFabricAppTask : Task
	{
		public string PackageLocation { get; set; }
		public string ClusterEndPoint { get; set; }
		public string ThumbPrint { get; set; }
		public string ProjectPath { get; set; }
		public string ParametersFile { get; set; }
		public ITaskItem[] Instances { get; set; }

		public override bool Execute()
		{
			//this.Log.LogWarning("attatch to " + Process.GetCurrentProcess().Id);

			//while (!Debugger.IsAttached)
			//{
			//	Thread.Sleep(1000);
			//}

			if (!string.IsNullOrWhiteSpace(ClusterEndPoint))
			{
				IServiceFabricClient client;
				if (!string.IsNullOrWhiteSpace(ThumbPrint))
				{
					Func<CancellationToken, System.Threading.Tasks.Task<SecuritySettings>> GetSecurityCredentials = (ct) =>
					{
						var clientCert = new X509Store(StoreName.My, StoreLocation.CurrentUser).Certificates.Find(X509FindType.FindByThumbprint, ThumbPrint, true)[0];
						var remoteSecuritySettings = new RemoteX509SecuritySettings(new List<string> { "server_cert_thumbprint" });
						return System.Threading.Tasks.Task.FromResult<SecuritySettings>(new X509SecuritySettings(clientCert, remoteSecuritySettings));
					};

					ExecuteAsync(new ServiceFabricClientBuilder().UseEndpoints(new Uri(ClusterEndPoint)).UseX509Security(GetSecurityCredentials)).Wait();
				}
				else
					ExecuteAsync(new ServiceFabricClientBuilder().UseEndpoints(new Uri(ClusterEndPoint))).Wait();
			}

			return true;
		}

		private async System.Threading.Tasks.Task ExecuteAsync(ServiceFabricClientBuilder clientbuilder)
		{
			var client = await clientbuilder.BuildAsyncDirect();
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
