using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceFabricSdkContrib.Common
{
public class ConsoleLogger : ILogger
    {
		public void LogInfo(string message)
		{
			Console.WriteLine(message);
		}

		public void LogError(string message, Exception exception)
		{
			Console.WriteLine(message);
		}

		public void LogWarning(string message)
		{
			Console.WriteLine(message);
		}

		public void LogVerbose(string message)
		{
			Console.WriteLine(message);
		}
	}
}
