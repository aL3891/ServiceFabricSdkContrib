using System;
using System.Collections;
using System.Management.Automation;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Client;
using Microsoft.ServiceFabric.Common;
using ServiceFabricSdkContrib.Common;

namespace ServiceFabricSdkContrib.Powershell
{
	[Cmdlet("New", "ServiceFabricServiceEx")]
	public class NewServiceFabricService : PSCmdlet
	{
		[Parameter(Mandatory = true)]
		public string ClusterEndPoint { get; set; }
		[Parameter]
		public string ThumbPrint { get; set; }


		[Parameter]
		public byte[] InitializationData { get; set; }

		[Parameter(Mandatory = true)]
		public string ApplicationId { get; set; }

		[Parameter(Mandatory = true)]
		public string ServiceName { get; set; }

		[Parameter(Mandatory = true)]
		public string ServiceType { get; set; }

		[Parameter]
		public SwitchParameter Stateless { get; set; }

		[Parameter]
		public SwitchParameter PersistedState { get; set; }

		[Parameter]
		public SwitchParameter PartitionSchemeUniformInt64 { get; set; }

		[Parameter]
		public SwitchParameter PartitionSchemeNamed { get; set; }

		[Parameter]
		public int PartitionCount { get; set; }
		[Parameter]
		public int InstanceCount { get; set; }
		[Parameter]
		public int MinReplicaSetSize { get; set; }
		[Parameter]
		public int TargetReplicaSetSize { get; set; }
		[Parameter]
		public long LowKey { get; set; }
		[Parameter]
		public long HighKey { get; set; }
		[Parameter]
		public string[] PartitionNames { get; set; }

		protected override void ProcessRecord()
		{
			PartitionSchemeDescription partitionSchemeDescription;
			ServiceDescription serviceDescription;
			var client = new ServiceFabricClientBuilder().ConnectAsync(ClusterEndPoint, ThumbPrint).Result;

			if (PartitionSchemeUniformInt64)
			{
				partitionSchemeDescription = new UniformInt64RangePartitionSchemeDescription(PartitionCount, LowKey.ToString(), HighKey.ToString());
			}
			else if (PartitionSchemeNamed)
			{
				partitionSchemeDescription = new NamedPartitionSchemeDescription(PartitionCount, PartitionNames);
			}
			else
			{
				partitionSchemeDescription = new SingletonPartitionSchemeDescription();
			}

			if (Stateless.IsPresent)
			{
				serviceDescription = new StatelessServiceDescription(new ServiceName(ServiceName), ServiceType, partitionSchemeDescription, InstanceCount, initializationData: InitializationData);
			}
			else
			{
				serviceDescription = new StatefulServiceDescription(new ServiceName(ServiceName), ServiceType, partitionSchemeDescription, TargetReplicaSetSize, MinReplicaSetSize, PersistedState, initializationData: InitializationData);
			}

			client.Services.CreateServiceAsync(ApplicationId, serviceDescription).Wait();
		}
	}
}
