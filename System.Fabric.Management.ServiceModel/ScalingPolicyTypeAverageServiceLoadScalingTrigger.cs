// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ScalingPolicyTypeAverageServiceLoadScalingTrigger
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
  public class ScalingPolicyTypeAverageServiceLoadScalingTrigger
  {
    private string metricNameField;
    private string lowerLoadThresholdField;
    private string upperLoadThresholdField;
    private string scaleIntervalInSecondsField;

    [XmlAttribute]
    public string MetricName
    {
      get
      {
        return this.metricNameField;
      }
      set
      {
        this.metricNameField = value;
      }
    }

    [XmlAttribute]
    public string LowerLoadThreshold
    {
      get
      {
        return this.lowerLoadThresholdField;
      }
      set
      {
        this.lowerLoadThresholdField = value;
      }
    }

    [XmlAttribute]
    public string UpperLoadThreshold
    {
      get
      {
        return this.upperLoadThresholdField;
      }
      set
      {
        this.upperLoadThresholdField = value;
      }
    }

    [XmlAttribute]
    public string ScaleIntervalInSeconds
    {
      get
      {
        return this.scaleIntervalInSecondsField;
      }
      set
      {
        this.scaleIntervalInSecondsField = value;
      }
    }
  }
}
