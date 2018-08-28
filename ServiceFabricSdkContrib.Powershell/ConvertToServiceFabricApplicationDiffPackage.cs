using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Client;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.Powershell
{
	[Cmdlet("ConvertTo", "ServiceFabricApplicationDiffPackage")]
	public class ConvertToServiceFabricApplicationDiffPackage : PSCmdlet
	{
		[Parameter(ValueFromPipeline = true, Position = 0)]
		public string[] PackagePaths { get; set; }

		protected override void ProcessRecord()
		{
			dynamic connection = GetVariableValue("ClusterConnection");
			if (connection == null)
				throw new ArgumentNullException("Service fabric connection not found");

			var logger = new PowershellLogger(this);
			var client = ServiceFabricClientFactory.Create(new Uri("http://localhost:19080"));

			var tt = Task.Run(() =>
			{
				var tasks = PackagePaths
					.Select(p => string.IsNullOrWhiteSpace(p) ? SessionState.Path.CurrentFileSystemLocation.Path : p)
					.Select(p => Path.IsPathRooted(p) ? p : Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, p))
					.Select(p => new { Path = p, Result = client.CreateDiffPackage(p) })
					.ToList();

				var res = tasks.Where(t => t.Result.Result).Select(t => t.Path).ToArray();
				logger.Stop();
				return res;
			});

			logger.Start();
			WriteObject(tt.Result);
		}
	}
}
