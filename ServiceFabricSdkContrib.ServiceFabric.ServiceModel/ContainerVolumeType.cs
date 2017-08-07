// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ContainerVolumeType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ContainerVolumeType
  {
    private DriverOptionType[] itemsField;
    private string sourceField;
    private string destinationField;
    private string driverField;
    private bool isReadOnlyField;

    [XmlElement("DriverOption")]
    public DriverOptionType[] Items
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
    public string Source
    {
      get
      {
        return this.sourceField;
      }
      set
      {
        this.sourceField = value;
      }
    }

    [XmlAttribute]
    public string Destination
    {
      get
      {
        return this.destinationField;
      }
      set
      {
        this.destinationField = value;
      }
    }

    [XmlAttribute]
    public string Driver
    {
      get
      {
        return this.driverField;
      }
      set
      {
        this.driverField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool IsReadOnly
    {
      get
      {
        return this.isReadOnlyField;
      }
      set
      {
        this.isReadOnlyField = value;
      }
    }

    public ContainerVolumeType()
    {
      this.isReadOnlyField = false;
    }
  }
}
