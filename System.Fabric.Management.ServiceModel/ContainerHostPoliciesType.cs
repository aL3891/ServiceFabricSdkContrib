// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ContainerHostPoliciesType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ContainerHostPoliciesType
  {
    private object[] itemsField;
    private string codePackageRefField;
    private string isolationField;
    private string hostnameField;
    private string containersRetentionCountField;
    private string runInteractiveField;

    public ContainerHostPoliciesType()
    {
      this.containersRetentionCountField = "0";
    }

    [XmlElement("CertificateRef", typeof (ContainerCertificateType))]
    [XmlElement("HealthConfig", typeof (ContainerHealthConfigType))]
    [XmlElement("ImageOverrides", typeof (ImageOverridesType))]
    [XmlElement("LogConfig", typeof (ContainerLoggingDriverType))]
    [XmlElement("NetworkConfig", typeof (ContainerNetworkConfigType))]
    [XmlElement("PortBinding", typeof (PortBindingType))]
    [XmlElement("RepositoryCredentials", typeof (RepositoryCredentialsType))]
    [XmlElement("SecurityOption", typeof (SecurityOptionsType))]
    [XmlElement("Volume", typeof (ContainerVolumeType))]
    public object[] Items
    {
      get
      {
        return this.itemsField;
      }
      set
      {
        this.itemsField = value;
      }
    }

    [XmlAttribute]
    public string CodePackageRef
    {
      get
      {
        return this.codePackageRefField;
      }
      set
      {
        this.codePackageRefField = value;
      }
    }

    [XmlAttribute]
    public string Isolation
    {
      get
      {
        return this.isolationField;
      }
      set
      {
        this.isolationField = value;
      }
    }

    [XmlAttribute]
    public string Hostname
    {
      get
      {
        return this.hostnameField;
      }
      set
      {
        this.hostnameField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string ContainersRetentionCount
    {
      get
      {
        return this.containersRetentionCountField;
      }
      set
      {
        this.containersRetentionCountField = value;
      }
    }

    [XmlAttribute]
    public string RunInteractive
    {
      get
      {
        return this.runInteractiveField;
      }
      set
      {
        this.runInteractiveField = value;
      }
    }
  }
}
