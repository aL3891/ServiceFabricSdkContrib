﻿// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceGroupTypeMember
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  [XmlRoot(IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServiceGroupTypeMember
  {
    private LoadMetricType[] loadMetricsField;
    private string serviceTypeNameField;

    [XmlArrayItem("LoadMetric", IsNullable = false)]
    public LoadMetricType[] LoadMetrics
    {
      get
      {
        return this.loadMetricsField;
      }
      set
      {
        this.loadMetricsField = value;
      }
    }

    [XmlAttribute]
    public string ServiceTypeName
    {
      get
      {
        return this.serviceTypeNameField;
      }
      set
      {
        this.serviceTypeNameField = value;
      }
    }
  }
}