// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ClusterManifestType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  [XmlRoot("ClusterManifest", IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ClusterManifestType
  {
    private ClusterManifestTypeNodeType[] nodeTypesField;
    private ClusterManifestTypeInfrastructure infrastructureField;
    private SettingsOverridesTypeSection[] fabricSettingsField;
    private ClusterManifestTypeCertificates certificatesField;
    private string nameField;
    private string versionField;
    private string descriptionField;

    [XmlArrayItem("NodeType", IsNullable = false)]
    public ClusterManifestTypeNodeType[] NodeTypes
    {
      get
      {
        return this.nodeTypesField;
      }
      set
      {
        this.nodeTypesField = value;
      }
    }

    public ClusterManifestTypeInfrastructure Infrastructure
    {
      get
      {
        return this.infrastructureField;
      }
      set
      {
        this.infrastructureField = value;
      }
    }

    [XmlArrayItem("Section", IsNullable = false)]
    public SettingsOverridesTypeSection[] FabricSettings
    {
      get
      {
        return this.fabricSettingsField;
      }
      set
      {
        this.fabricSettingsField = value;
      }
    }

    public ClusterManifestTypeCertificates Certificates
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

    [XmlAttribute]
    public string Version
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

    [XmlAttribute]
    public string Description
    {
      get
      {
        return this.descriptionField;
      }
      set
      {
        this.descriptionField = value;
      }
    }
  }
}
