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

		[Parameter(Mandatory = true)]
		public string ClusterEndPoint { get; set; }

		[Parameter]
		public string ThumbPrint { get; set; }

		protected override void ProcessRecord()
		{
			var logger = new PowershellLogger(this);
			var res = ExecuteAsync(logger, ClusterEndPoint, ThumbPrint, PackagePaths);
			logger.Start();
			WriteObject(res.Result);
		}

		private async Task<string[]> ExecuteAsync(PowershellLogger logger, string clusterEndPoint, string thumbPrint, string[] packagePaths)
		{
			try
			{
				var client = await new ServiceFabricClientBuilder().ConnectAsync(clusterEndPoint, thumbPrint);
				var tasks = packagePaths
					.Select(p => string.IsNullOrWhiteSpace(p) ? SessionState.Path.CurrentFileSystemLocation.Path : p)
					.Select(p => Path.IsPathRooted(p) ? p : Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, p))
					.Select(async p => new { Path = p, Result = await client.CreateDiffPackage(p) });

				var res = await Task.WhenAll(tasks);
				return res.Where(t => t.Result).Select(t => t.Path).ToArray();
			}
			catch (Exception e)
			{
				logger.LogError(e.Message, e);
			}
			finally
			{
				logger.Stop();
			}

			return null;
		}
	}
}
