// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.DebugParametersType
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
  public class DebugParametersType
  {
    private string[] containerEntryPointField;
    private string[] containerMountedVolumeField;
    private string[] containerEnvironmentBlockField;
    private string programExePathField;
    private string argumentsField;
    private DebugParametersTypeEntryPointType entryPointTypeField;
    private string codePackageLinkFolderField;
    private string configPackageLinkFolderField;
    private string dataPackageLinkFolderField;
    private string lockFileField;
    private string workingFolderField;
    private string debugParametersFileField;
    private string environmentBlockField;

    public DebugParametersType()
    {
      this.entryPointTypeField = DebugParametersTypeEntryPointType.Main;
    }

    [XmlElement("ContainerEntryPoint")]
    public string[] ContainerEntryPoint
    {
      get
      {
        return this.containerEntryPointField;
      }
      set
      {
        this.containerEntryPointField = value;
      }
    }

    [XmlElement("ContainerMountedVolume")]
    public string[] ContainerMountedVolume
    {
      get
      {
        return this.containerMountedVolumeField;
      }
      set
      {
        this.containerMountedVolumeField = value;
      }
    }

    [XmlElement("ContainerEnvironmentBlock")]
    public string[] ContainerEnvironmentBlock
    {
      get
      {
        return this.containerEnvironmentBlockField;
      }
      set
      {
        this.containerEnvironmentBlockField = value;
      }
    }

    [XmlAttribute]
    public string ProgramExePath
    {
      get
      {
        return this.programExePathField;
      }
      set
      {
        this.programExePathField = value;
      }
    }

    [XmlAttribute]
    public string Arguments
    {
      get
      {
        return this.argumentsField;
      }
      set
      {
        this.argumentsField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(DebugParametersTypeEntryPointType.Main)]
    public DebugParametersTypeEntryPointType EntryPointType
    {
      get
      {
        return this.entryPointTypeField;
      }
      set
      {
        this.entryPointTypeField = value;
      }
    }

    [XmlAttribute]
    public string CodePackageLinkFolder
    {
      get
      {
        return this.codePackageLinkFolderField;
      }
      set
      {
        this.codePackageLinkFolderField = value;
      }
    }

    [XmlAttribute]
    public string ConfigPackageLinkFolder
    {
      get
      {
        return this.configPackageLinkFolderField;
      }
      set
      {
        this.configPackageLinkFolderField = value;
      }
    }

    [XmlAttribute]
    public string DataPackageLinkFolder
    {
      get
      {
        return this.dataPackageLinkFolderField;
      }
      set
      {
        this.dataPackageLinkFolderField = value;
      }
    }

    [XmlAttribute]
    public string LockFile
    {
      get
      {
        return this.lockFileField;
      }
      set
      {
        this.lockFileField = value;
      }
    }

    [XmlAttribute]
    public string WorkingFolder
    {
      get
      {
        return this.workingFolderField;
      }
      set
      {
        this.workingFolderField = value;
      }
    }

    [XmlAttribute]
    public string DebugParametersFile
    {
      get
      {
        return this.debugParametersFileField;
      }
      set
      {
        this.debugParametersFileField = value;
      }
    }

    [XmlAttribute]
    public string EnvironmentBlock
    {
      get
      {
        return this.environmentBlockField;
      }
      set
      {
        this.environmentBlockField = value;
      }
    }
  }
}
