// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ApplicationInstanceType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  [XmlRoot("ApplicationInstance", IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ApplicationInstanceType
  {
    private ApplicationInstanceTypeApplicationPackageRef applicationPackageRefField;
    private ApplicationInstanceTypeServicePackageRef[] servicePackageRefField;
    private ServiceType[] serviceTemplatesField;
    private DefaultServicesType defaultServicesField;
    private int versionField;
    private string nameUriField;
    private string applicationIdField;
    private string applicationTypeNameField;
    private string applicationTypeVersionField;
    private XmlAttribute[] anyAttrField;

    public ApplicationInstanceTypeApplicationPackageRef ApplicationPackageRef
    {
      get
      {
        return this.applicationPackageRefField;
      }
      set
      {
        this.applicationPackageRefField = value;
      }
    }

    [XmlElement("ServicePackageRef")]
    public ApplicationInstanceTypeServicePackageRef[] ServicePackageRef
    {
      get
      {
        return this.servicePackageRefField;
      }
      set
      {
        this.servicePackageRefField = value;
      }
    }

    [XmlArrayItem("StatefulService", typeof (StatefulServiceType), IsNullable = false)]
    [XmlArrayItem("StatefulServiceGroup", typeof (StatefulServiceGroupType), IsNullable = false)]
    [XmlArrayItem("StatelessService", typeof (StatelessServiceType), IsNullable = false)]
    [XmlArrayItem("StatelessServiceGroup", typeof (StatelessServiceGroupType), IsNullable = false)]
    public ServiceType[] ServiceTemplates
    {
      get
      {
        return this.serviceTemplatesField;
      }
      set
      {
        this.serviceTemplatesField = value;
      }
    }

    public DefaultServicesType DefaultServices
    {
      get
      {
        return this.defaultServicesField;
      }
      set
      {
        this.defaultServicesField = value;
      }
    }

    [XmlAttribute]
    public int Version
    {
      get
      {
        return this.versionField;
      }
      set
      {
        this.versionField = value;
      }
    }

    [XmlAttribute(DataType = "anyURI")]
    public string NameUri
    {
      get
      {
        return this.nameUriField;
      }
      set
      {
        this.nameUriField = value;
      }
    }

    [XmlAttribute]
    public string ApplicationId
    {
      get
      {
        return this.applicationIdField;
      }
      set
      {
        this.applicationIdField = value;
      }
    }

    [XmlAttribute]
    public string ApplicationTypeName
    {
      get
      {
        return this.applicationTypeNameField;
      }
      set
      {
        this.applicationTypeNameField = value;
      }
    }

    [XmlAttribute]
    public string ApplicationTypeVersion
    {
      get
      {
        return this.applicationTypeVersionField;
      }
      set
      {
        this.applicationTypeVersionField = value;
      }
    }

    [XmlAnyAttribute]
    public XmlAttribute[] AnyAttr
    {
      get
      {
        return this.anyAttrField;
      }
      set
      {
        this.anyAttrField = value;
      }
    }
  }
}
