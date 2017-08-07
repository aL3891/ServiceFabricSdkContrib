// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.StatefulServiceType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [XmlInclude(typeof (StatefulServiceGroupType))]
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class StatefulServiceType : ServiceType
  {
    private string targetReplicaSetSizeField;
    private string minReplicaSetSizeField;
    private string replicaRestartWaitDurationSecondsField;
    private string quorumLossWaitDurationSecondsField;
    private string standByReplicaKeepDurationSecondsField;

    [XmlAttribute]
    [DefaultValue("1")]
    public string TargetReplicaSetSize
    {
      get
      {
        return this.targetReplicaSetSizeField;
      }
      set
      {
        this.targetReplicaSetSizeField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("1")]
    public string MinReplicaSetSize
    {
      get
      {
        return this.minReplicaSetSizeField;
      }
      set
      {
        this.minReplicaSetSizeField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("")]
    public string ReplicaRestartWaitDurationSeconds
    {
      get
      {
        return this.replicaRestartWaitDurationSecondsField;
      }
      set
      {
        this.replicaRestartWaitDurationSecondsField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("")]
    public string QuorumLossWaitDurationSeconds
    {
      get
      {
        return this.quorumLossWaitDurationSecondsField;
      }
      set
      {
        this.quorumLossWaitDurationSecondsField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("")]
    public string StandByReplicaKeepDurationSeconds
    {
      get
      {
        return this.standByReplicaKeepDurationSecondsField;
      }
      set
      {
        this.standByReplicaKeepDurationSecondsField = value;
      }
    }

    public StatefulServiceType()
    {
      this.targetReplicaSetSizeField = "1";
      this.minReplicaSetSizeField = "1";
      this.replicaRestartWaitDurationSecondsField = "";
      this.quorumLossWaitDurationSecondsField = "";
      this.standByReplicaKeepDurationSecondsField = "";
    }
  }
}
