// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceTypeHealthPolicyType
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
  public class ServiceTypeHealthPolicyType
  {
    private string maxPercentUnhealthyServicesField;
    private string maxPercentUnhealthyPartitionsPerServiceField;
    private string maxPercentUnhealthyReplicasPerPartitionField;

    public ServiceTypeHealthPolicyType()
    {
      this.maxPercentUnhealthyServicesField = "0";
      this.maxPercentUnhealthyPartitionsPerServiceField = "0";
      this.maxPercentUnhealthyReplicasPerPartitionField = "0";
    }

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
  }
}
