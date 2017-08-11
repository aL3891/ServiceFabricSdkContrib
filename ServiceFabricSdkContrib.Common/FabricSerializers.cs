using System;
using System.Collections.Generic;
using System.Fabric.Management.ServiceModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.Common
{
	public class FabricSerializers
	{
		static XmlSerializer appManifestSerializer = new XmlSerializer(typeof(ApplicationManifestType));
		static XmlSerializer serviceManifestSerializer = new XmlSerializer(typeof(ServiceManifestType));
		static XmlSerializer clusterManifestSerializer = new XmlSerializer(typeof(ClusterManifestType));

		public static ClusterManifestType ClusterManifestFromFile(string path)
		{
			using (var stream = new StreamReader(path))
				return (ClusterManifestType)clusterManifestSerializer.Deserialize(stream);
		}

		public static ClusterManifestType ClusterManifestFromString(string g)
		{
			using (var stream = new StringReader(g))
				return (ClusterManifestType)clusterManifestSerializer.Deserialize(stream);
		}

		public static ApplicationManifestType AppManifestFromFile(string path)
		{
			using (var stream = new StreamReader(path))
				return (ApplicationManifestType)appManifestSerializer.Deserialize(stream);
		}

		public static ApplicationManifestType AppManifestFromString(string g)
		{
			using (var stream = new StringReader(g))
				return (ApplicationManifestType)appManifestSerializer.Deserialize(stream);
		}

		public static ServiceManifestType ServiceManifestFromFile(string g)
		{
			using (var stream = new StreamReader(g))
				return (ServiceManifestType)serviceManifestSerializer.Deserialize(stream);
		}

		public static ServiceManifestType ServiceManifestFromString(string g)
		{
			using (var stream = new StringReader(g))
				return (ServiceManifestType)serviceManifestSerializer.Deserialize(stream);
		}

		public static void SaveServiceManifest(string v, ServiceManifestType srv)
		{
			if (File.Exists(v))
				File.Delete(v);

			using (var stream = new StreamWriter(v))
				serviceManifestSerializer.Serialize(stream, srv);
		}

		public static void SaveAppManifest(string v, ApplicationManifestType appManifest)
		{
			if (File.Exists(v))
				File.Delete(v);

			using (var stream = new StreamWriter(v))
				appManifestSerializer.Serialize(stream, appManifest);
		}
	}
}
