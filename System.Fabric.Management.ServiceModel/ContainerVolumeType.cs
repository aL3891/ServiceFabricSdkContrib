// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ContainerVolumeType
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
  public class ContainerVolumeType
  {
    private DriverOptionType[] itemsField;
    private string sourceField;
    private string destinationField;
    private string driverField;
    private bool isReadOnlyField;

    public ContainerVolumeType()
    {
      this.isReadOnlyField = false;
    }

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
  }
}
