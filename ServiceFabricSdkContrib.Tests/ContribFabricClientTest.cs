using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceFabricSdkContrib.Common;
using ServiceFabricSdkContrib.MsBuild;

namespace ServiceFabricSdkContrib.Tests
{
	[TestClass]
	public class ContribFabricClientTest
	{
		string basePath = Path.Combine(new DirectoryInfo(typeof(ContribFabricClientTest).Assembly.Location).Parent.Parent.Parent.Parent.Parent.FullName, "TestSolution2");
		private IServiceFabricClient client;

		[TestInitialize]
		public async Task Init()
		{
			Process.Start(new ProcessStartInfo { FileName = "dotnet", Arguments = "publish", WorkingDirectory = basePath }).WaitForExit();
			client = ServiceFabricClientFactory.Create(new Uri("http://localhost:19080"));

			var apps = await client.Applications.GetApplicationInfoListAsync();
			foreach (var app in apps.Data)
			{
				await client.Applications.DeleteApplicationAsync(app.Id);
			}

			var apptypes = await client.ApplicationTypes.GetApplicationTypeInfoListAsync();

			foreach (var type in apptypes.Data)
			{
				await client.ApplicationTypes.UnprovisionApplicationTypeAsync(type.Name, new UnprovisionApplicationTypeDescriptionInfo(type.Version));
			}
		}

		[TestMethod]
		public async Task CreateDiffPackageTest()
		{
			await client.CreateDiffPackage(Path.Combine(basePath, "TestApplication1\\pkg\\Debug"));
		}

		[TestMethod]
		public async Task DeploySolutionTest()
		{
			await client.DeployServiceFabricSolution(new ServiceFabricSolution
			{
				Applications = {
					new ServiceFabricApplicationSpec {
						 Name = "TestApp1",
						 PackagePath = Path.Combine(basePath, "TestApplication1\\pkg\\Debug"),
					}
				 }
			}, false);
		}

		[TestMethod]
		public async Task DeployDiffSolutionTest()
		{
			await client.CreateDiffPackage(Path.Combine(basePath, "TestApplication1\\pkg\\Debug"));
		}

		[TestMethod]
		public void DeployWithMsBuild()
		{
			var target = new PublishFabricAppTask
			{
				ProjectPath = Path.Combine(basePath, "TestApplication1\\TestApplication1.sfproj"),
				PackageLocation = "pkg\\Debug",
				ClusterEndPoint = "http://localhost:19080",
				Instances = new[] { new Microsoft.Build.Utilities.TaskItem("TestApplication1") },
				ParametersFile = @"ApplicationParameters\Local.1Node.xml",
				ThumbPrint = ""
			};

			target.Execute();
		}
	}
}
