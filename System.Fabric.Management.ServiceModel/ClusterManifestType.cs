﻿// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ClusterManifestType
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