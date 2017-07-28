// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ClusterManifestTypeNodeType
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ClusterManifestTypeNodeType
  {
    private FabricEndpointsType endpointsField;
    private FabricKtlLoggerSettingsType ktlLoggerSettingsField;
    private CertificatesType certificatesField;
    private KeyValuePairType[] placementPropertiesField;
    private KeyValuePairType[] capacitiesField;
    private string nameField;

    public FabricEndpointsType Endpoints
    {
      get
      {
        return this.endpointsField;
      }
      set
      {
        this.endpointsField = value;
      }
    }

    public FabricKtlLoggerSettingsType KtlLoggerSettings
    {
      get
      {
        return this.ktlLoggerSettingsField;
      }
      set
      {
        this.ktlLoggerSettingsField = value;
      }
    }

    public CertificatesType Certificates
    {
      get
      {
        return this.certificatesField;
      }
      set
      {
        this.certificatesField = value;
      }
    }

    [XmlArrayItem("Property", IsNullable = false)]
    public KeyValuePairType[] PlacementProperties
    {
      get
      {
        return this.placementPropertiesField;
      }
      set
      {
        this.placementPropertiesField = value;
      }
    }

    [XmlArrayItem("Capacity", IsNullable = false)]
    public KeyValuePairType[] Capacities
    {
      get
      {
        return this.capacitiesField;
      }
      set
      {
        this.capacitiesField = value;
      }
    }

    [XmlAttribute]
    public string Name
    {
      get
      {
        return this.nameField;
      }
      set
      {
        this.nameField = value;
      }
    }
  }
}
