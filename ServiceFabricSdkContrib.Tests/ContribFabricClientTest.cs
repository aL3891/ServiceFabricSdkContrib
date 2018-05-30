using Microsoft.ServiceFabric.Client;
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
		string BasePath = Path.Combine(new DirectoryInfo(typeof(CommonTest).Assembly.Location).Parent.Parent.Parent.Parent.Parent.FullName, "TestSolution2");

		[TestMethod]
		public async Task CreateDiffPackageTest()
		{
			Process.Start(new ProcessStartInfo { FileName = "dotnet", Arguments = "publish", WorkingDirectory = BasePath }).WaitForExit();
			var client = new ContribFabricClient(ServiceFabricClientFactory.Create(new Uri("http://localhost:19080")), new ConsoleLogger());
			await client.CreateDiffPackage(Path.Combine(BasePath, "TestApplication1\\pkg\\Debug"));
		}

		[TestMethod]
		public async Task DeploySolutionTest()
		{
			Process.Start(new ProcessStartInfo { FileName = "dotnet", Arguments = "publish", WorkingDirectory = BasePath }).WaitForExit();
			var client = new ContribFabricClient(ServiceFabricClientFactory.Create(new Uri("http://localhost:19080")), new ConsoleLogger());
			await client.DeployServiceFabricSolution(new ServiceFabricSolution
			{
				Applications =  {
					new ServiceFabricApplicationSpec{
						 PackagePath =Path.Combine(BasePath, "TestApplication1\\pkg\\Debug"), 
					}
				 }
			}, false);
		}

		[TestMethod]
		public async Task DeployDiffSolutionTest()
		{
			Process.Start(new ProcessStartInfo { FileName = "dotnet", Arguments = "publish", WorkingDirectory = BasePath }).WaitForExit();
			var client = new ContribFabricClient(ServiceFabricClientFactory.Create(new Uri("http://localhost:19080")), new ConsoleLogger());
			await client.CreateDiffPackage(Path.Combine(BasePath, "TestApplication1\\pkg\\Debug"));
		}
	}
}
