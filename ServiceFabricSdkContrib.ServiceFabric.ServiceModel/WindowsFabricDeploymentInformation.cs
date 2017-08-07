// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.WindowsFabricDeploymentInformation
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class WindowsFabricDeploymentInformation
  {
    private string instanceIdField;
    private string mSILocationField;
    private string clusterManifestLocationField;
    private string infrastructureManifestLocationField;
    private string targetVersionField;
    private string nodeNameField;
    private bool removeNodeStateField;
    private string upgradeEntryPointExeField;
    private string upgradeEntryPointExeParametersField;
    private string undoUpgradeEntryPointExeField;
    private string undoUpgradeEntryPointExeParametersField;

    [XmlAttribute]
    public string InstanceId
    {
      get
      {
        return this.instanceIdField;
      }
      set
      {
        this.instanceIdField = value;
      }
    }

    [XmlAttribute]
    public string MSILocation
    {
      get
      {
        return this.mSILocationField;
      }
      set
      {
        this.mSILocationField = value;
      }
    }

    [XmlAttribute]
    public string ClusterManifestLocation
    {
      get
      {
        return this.clusterManifestLocationField;
      }
      set
      {
        this.clusterManifestLocationField = value;
      }
    }

    [XmlAttribute]
    public string InfrastructureManifestLocation
    {
      get
      {
        return this.infrastructureManifestLocationField;
      }
      set
      {
        this.infrastructureManifestLocationField = value;
      }
    }

    [XmlAttribute]
    public string TargetVersion
    {
      get
      {
        return this.targetVersionField;
      }
      set
      {
        this.targetVersionField = value;
      }
    }

    [XmlAttribute]
    public string NodeName
    {
      get
      {
        return this.nodeNameField;
      }
      set
      {
        this.nodeNameField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool RemoveNodeState
    {
      get
      {
        return this.removeNodeStateField;
      }
      set
      {
        this.removeNodeStateField = value;
      }
    }

    [XmlAttribute]
    public string UpgradeEntryPointExe
    {
      get
      {
        return this.upgradeEntryPointExeField;
      }
      set
      {
        this.upgradeEntryPointExeField = value;
      }
    }

    [XmlAttribute]
    public string UpgradeEntryPointExeParameters
    {
      get
      {
        return this.upgradeEntryPointExeParametersField;
      }
      set
      {
        this.upgradeEntryPointExeParametersField = value;
      }
    }

    [XmlAttribute]
    public string UndoUpgradeEntryPointExe
    {
      get
      {
        return this.undoUpgradeEntryPointExeField;
      }
      set
      {
        this.undoUpgradeEntryPointExeField = value;
      }
    }

    [XmlAttribute]
    public string UndoUpgradeEntryPointExeParameters
    {
      get
      {
        return this.undoUpgradeEntryPointExeParametersField;
      }
      set
      {
        this.undoUpgradeEntryPointExeParametersField = value;
      }
    }

    public WindowsFabricDeploymentInformation()
    {
      this.removeNodeStateField = false;
    }
  }
}
