using System;

namespace ServiceFabricSdkContrib.Common
{
	public interface ILogger
	{
		void LogInfo(string message);
		void LogError(string message, Exception exception);
		void LogWarning(string message);
		void LogVerbose(string message);
	}
}
