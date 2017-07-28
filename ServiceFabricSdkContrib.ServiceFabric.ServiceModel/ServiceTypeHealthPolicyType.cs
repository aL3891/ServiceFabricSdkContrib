// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceTypeHealthPolicyType
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServiceTypeHealthPolicyType
  {
    private string maxPercentUnhealthyServicesField;
    private string maxPercentUnhealthyPartitionsPerServiceField;
    private string maxPercentUnhealthyReplicasPerPartitionField;

    [XmlAttribute]
    [DefaultValue("0")]
    public string MaxPercentUnhealthyServices
    {
      get
      {
        return this.maxPercentUnhealthyServicesField;
      }
      set
      {
        this.maxPercentUnhealthyServicesField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MaxPercentUnhealthyPartitionsPerService
    {
      get
      {
        return this.maxPercentUnhealthyPartitionsPerServiceField;
      }
      set
      {
        this.maxPercentUnhealthyPartitionsPerServiceField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MaxPercentUnhealthyReplicasPerPartition
    {
      get
      {
        return this.maxPercentUnhealthyReplicasPerPartitionField;
      }
      set
      {
        this.maxPercentUnhealthyReplicasPerPartitionField = value;
      }
    }

    public ServiceTypeHealthPolicyType()
    {
      this.maxPercentUnhealthyServicesField = "0";
      this.maxPercentUnhealthyPartitionsPerServiceField = "0";
      this.maxPercentUnhealthyReplicasPerPartitionField = "0";
    }
  }
}
