// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.DefaultServicesTypeService
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
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class DefaultServicesTypeService
  {
    private ServiceType itemField;
    private string nameField;
    private string generatedIdRefField;
    private string serviceDnsNameField;
    private string servicePackageActivationModeField;

    public DefaultServicesTypeService()
    {
      this.servicePackageActivationModeField = "SharedProcess";
    }

    [XmlElement("StatefulService", typeof (StatefulServiceType))]
    [XmlElement("StatelessService", typeof (StatelessServiceType))]
    public ServiceType Item
    {
      get
      {
        return this.itemField;
      }
      set
      {
        this.itemField = value;
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
    public string GeneratedIdRef
    {
      get
      {
        return this.generatedIdRefField;
      }
      set
      {
        this.generatedIdRefField = value;
      }
    }

    [XmlAttribute]
    public string ServiceDnsName
    {
      get
      {
        return this.serviceDnsNameField;
      }
      set
      {
        this.serviceDnsNameField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("SharedProcess")]
    public string ServicePackageActivationMode
    {
      get
      {
        return this.servicePackageActivationModeField;
      }
      set
      {
        this.servicePackageActivationModeField = value;
      }
    }
  }
}
