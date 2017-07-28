// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceTypeUniformInt64Partition
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
  public class ServiceTypeUniformInt64Partition
  {
    private string partitionCountField;
    private string lowKeyField;
    private string highKeyField;

    [XmlAttribute]
    public string PartitionCount
    {
      get
      {
        return this.partitionCountField;
      }
      set
      {
        this.partitionCountField = value;
      }
    }

    [XmlAttribute]
    public string LowKey
    {
      get
      {
        return this.lowKeyField;
      }
      set
      {
        this.lowKeyField = value;
      }
    }

    [XmlAttribute]
    public string HighKey
    {
      get
      {
        return this.highKeyField;
      }
      set
      {
        this.highKeyField = value;
      }
    }
  }
}
