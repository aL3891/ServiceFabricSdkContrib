// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ResourceGovernancePolicyType
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
  public class ResourceGovernancePolicyType
  {
    private string codePackageRefField;
    private string memoryInMBField;
    private string memorySwapInMBField;
    private string memoryReservationInMBField;
    private string cpuSharesField;
    private string cpuPercentField;
    private string maximumIOpsField;
    private string maximumIOBandwidthField;
    private string blockIOWeightField;

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
    [DefaultValue("0")]
    public string MemoryInMB
    {
      get
      {
        return this.memoryInMBField;
      }
      set
      {
        this.memoryInMBField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MemorySwapInMB
    {
      get
      {
        return this.memorySwapInMBField;
      }
      set
      {
        this.memorySwapInMBField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MemoryReservationInMB
    {
      get
      {
        return this.memoryReservationInMBField;
      }
      set
      {
        this.memoryReservationInMBField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string CpuShares
    {
      get
      {
        return this.cpuSharesField;
      }
      set
      {
        this.cpuSharesField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string CpuPercent
    {
      get
      {
        return this.cpuPercentField;
      }
      set
      {
        this.cpuPercentField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MaximumIOps
    {
      get
      {
        return this.maximumIOpsField;
      }
      set
      {
        this.maximumIOpsField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MaximumIOBandwidth
    {
      get
      {
        return this.maximumIOBandwidthField;
      }
      set
      {
        this.maximumIOBandwidthField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string BlockIOWeight
    {
      get
      {
        return this.blockIOWeightField;
      }
      set
      {
        this.blockIOWeightField = value;
      }
    }

    public ResourceGovernancePolicyType()
    {
      this.memoryInMBField = "0";
      this.memorySwapInMBField = "0";
      this.memoryReservationInMBField = "0";
      this.cpuSharesField = "0";
      this.cpuPercentField = "0";
      this.maximumIOpsField = "0";
      this.maximumIOBandwidthField = "0";
      this.blockIOWeightField = "0";
    }
  }
}
