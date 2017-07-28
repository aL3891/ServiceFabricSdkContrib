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
    public class Helper
    {
        static XmlSerializer serializer = new XmlSerializer(typeof(ApplicationManifestType));
        static XmlSerializer x = new XmlSerializer(typeof(ServiceManifestType));

        public static ApplicationManifestType FromFile(string path)
        {
            using (var stream = new StreamReader(path))
                return (ApplicationManifestType)serializer.Deserialize(stream);
        }

        public static ApplicationManifestType FromString(string g)
        {
            using (var stream = new StringReader(g))
                return (ApplicationManifestType)serializer.Deserialize(stream);
        }

        public static ServiceManifestType serviceFromFile(string g)
        {
            using (var stream = new StreamReader(g))
                return (ServiceManifestType)x.Deserialize(stream);
        }

        public static ServiceManifestType serviceFromString(string g)
        {
            using (var stream = new StringReader(g))
                return (ServiceManifestType)x.Deserialize(stream);
        }

        public static void SaveService(string v, ServiceManifestType srv)
        {
            using (var stream = new StreamWriter(v))
                x.Serialize(stream, srv);
        }

        public static void SaveApp(string v, ApplicationManifestType appManifest)
        {
            using (var stream = new StreamWriter(v))
                serializer.Serialize(stream, appManifest);
        }
    }
}
