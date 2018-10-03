using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Utilities;

namespace ServiceFabricSdkContrib.MsBuild
{
	public class PublishFabricAppTask : Task
	{
		public string PackageLocation { get; set; }
		public string ClusterEndPoint { get; set; }
		public string ThumbPrint { get; set; }

		public override bool Execute()
		{
			throw new NotImplementedException();
		}
	}
}
