// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ClusterManifestTypeNodeType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

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
    private LogicalDirectoryType[] logicalDirectoriesField;
    private CertificatesType certificatesField;
    private KeyValuePairType[] placementPropertiesField;
    private KeyValuePairType[] capacitiesField;
    private KeyValuePairType[] sfssRgPoliciesField;
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

    [XmlArrayItem("LogicalDirectory", IsNullable = false)]
    public LogicalDirectoryType[] LogicalDirectories
    {
      get
      {
        return this.logicalDirectoriesField;
      }
      set
      {
        this.logicalDirectoriesField = value;
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

    [XmlArrayItem("SfssRgPolicy", IsNullable = false)]
    public KeyValuePairType[] SfssRgPolicies
    {
      get
      {
        return this.sfssRgPoliciesField;
      }
      set
      {
        this.sfssRgPoliciesField = value;
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
