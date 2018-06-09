// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.StatefulServiceType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
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

    public StatefulServiceType()
    {
      this.targetReplicaSetSizeField = "1";
      this.minReplicaSetSizeField = "1";
      this.replicaRestartWaitDurationSecondsField = "";
      this.quorumLossWaitDurationSecondsField = "";
      this.standByReplicaKeepDurationSecondsField = "";
    }

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
  }
}
