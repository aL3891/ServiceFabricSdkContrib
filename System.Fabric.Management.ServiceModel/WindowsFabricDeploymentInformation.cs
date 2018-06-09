// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.WindowsFabricDeploymentInformation
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

    public WindowsFabricDeploymentInformation()
    {
      this.removeNodeStateField = false;
    }

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
  }
}
