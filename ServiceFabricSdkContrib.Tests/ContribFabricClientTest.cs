using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceFabricSdkContrib.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ServiceFabricSdkContrib.Tests
{
	[TestClass]
	public class ContribFabricClientTest
	{
		string BasePath = Path.Combine(new DirectoryInfo(typeof(ContribFabricClientTest).Assembly.Location).Parent.Parent.Parent.Parent.Parent.FullName, "TestSolution2");
		private ContribFabricClient client;

		[TestInitialize]
		public async Task Init()
		{
			Process.Start(new ProcessStartInfo { FileName = "dotnet", Arguments = "publish", WorkingDirectory = BasePath }).WaitForExit();
			client = new ContribFabricClient(ServiceFabricClientFactory.Create(new Uri("http://localhost:19080")), new ConsoleLogger());

			var apps = await client.Client.Applications.GetApplicationInfoListAsync();
			foreach (var app in apps.Data)
			{
				await client.Client.Applications.DeleteApplicationAsync(app.Id);
			}

			var apptypes = await client.Client.ApplicationTypes.GetApplicationTypeInfoListAsync();

			foreach (var type in apptypes.Data)
			{
				await client.Client.ApplicationTypes.UnprovisionApplicationTypeAsync(type.Name, new UnprovisionApplicationTypeDescriptionInfo(type.Version));
			}
		}

		[TestMethod]
		public async Task CreateDiffPackageTest()
		{
			await client.CreateDiffPackage(Path.Combine(BasePath, "TestApplication1\\pkg\\Debug"));
		}

		[TestMethod]
		public async Task DeploySolutionTest()
		{
			await client.DeployServiceFabricSolution(new ServiceFabricSolution
			{
				Applications =  {
					new ServiceFabricApplicationSpec{
						 Name ="TestApp1",
						 PackagePath =Path.Combine(BasePath, "TestApplication1\\pkg\\Debug"),
					}
				 }
			}, false);
		}

		[TestMethod]
		public async Task DeployDiffSolutionTest()
		{
			await client.CreateDiffPackage(Path.Combine(BasePath, "TestApplication1\\pkg\\Debug"));
		}
	}
}
