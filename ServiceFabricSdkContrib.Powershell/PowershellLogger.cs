using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.Powershell
{
	public class PowershellLogger : ILogger
	{
		private BlockingCollection<Action<PSCmdlet>> logs = new BlockingCollection<Action<PSCmdlet>>();
		PSCmdlet Target { get; set; }

		public PowershellLogger(PSCmdlet target)
		{
			Target = target;
		}

		public void Stop()
		{
			logs.CompleteAdding();
		}

		public void Start()
		{
			foreach (var log in logs.GetConsumingEnumerable())
			{
				log(Target);
			}
		}

		public void LogInfo(string message)
		{
			logs.Add(p => p.WriteObject(message));
		}

		public void LogError(string message, Exception exception)
		{
			logs.Add(p => p.WriteError(new ErrorRecord(exception, exception.Message, ErrorCategory.NotSpecified, p)));
		}

		public void LogWarning(string message)
		{
			logs.Add(p => p.WriteWarning(message));
		}

		public void LogVerbose(string message)
		{
			logs.Add(p => p.WriteVerbose(message));
		}
	}
}
