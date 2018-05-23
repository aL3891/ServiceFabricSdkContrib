// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ScalingPolicyTypeAddRemoveIncrementalNamedPartitionScalingMechanism
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
  public class ScalingPolicyTypeAddRemoveIncrementalNamedPartitionScalingMechanism
  {
    private string minPartitionCountField;
    private string maxPartitionCountField;
    private string scaleIncrementField;

    [XmlAttribute]
    public string MinPartitionCount
    {
      get
      {
        return this.minPartitionCountField;
      }
      set
      {
        this.minPartitionCountField = value;
      }
    }

    [XmlAttribute]
    public string MaxPartitionCount
    {
      get
      {
        return this.maxPartitionCountField;
      }
      set
      {
        this.maxPartitionCountField = value;
      }
    }

    [XmlAttribute]
    public string ScaleIncrement
    {
      get
      {
        return this.scaleIncrementField;
      }
      set
      {
        this.scaleIncrementField = value;
      }
    }
  }
}
