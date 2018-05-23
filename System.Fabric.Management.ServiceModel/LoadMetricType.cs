// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.LoadMetricType
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
  public class LoadMetricType
  {
    private string nameField;
    private long defaultLoadField;
    private long primaryDefaultLoadField;
    private long secondaryDefaultLoadField;
    private LoadMetricTypeWeight weightField;
    private bool weightFieldSpecified;

    public LoadMetricType()
    {
      this.defaultLoadField = 0L;
      this.primaryDefaultLoadField = 0L;
      this.secondaryDefaultLoadField = 0L;
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
    [DefaultValue(typeof (long), "0")]
    public long DefaultLoad
    {
      get
      {
        return this.defaultLoadField;
      }
      set
      {
        this.defaultLoadField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(typeof (long), "0")]
    public long PrimaryDefaultLoad
    {
      get
      {
        return this.primaryDefaultLoadField;
      }
      set
      {
        this.primaryDefaultLoadField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(typeof (long), "0")]
    public long SecondaryDefaultLoad
    {
      get
      {
        return this.secondaryDefaultLoadField;
      }
      set
      {
        this.secondaryDefaultLoadField = value;
      }
    }

    [XmlAttribute]
    public LoadMetricTypeWeight Weight
    {
      get
      {
        return this.weightField;
      }
      set
      {
        this.weightField = value;
      }
    }

    [XmlIgnore]
    public bool WeightSpecified
    {
      get
      {
        return this.weightFieldSpecified;
      }
      set
      {
        this.weightFieldSpecified = value;
      }
    }
  }
}
