// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ExeHostEntryPointType
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
  public class ExeHostEntryPointType
  {
    private string programField;
    private string argumentsField;
    private ExeHostEntryPointTypeWorkingFolder workingFolderField;
    private ExeHostEntryPointTypeConsoleRedirection consoleRedirectionField;
    private bool isExternalExecutableField;

    public ExeHostEntryPointType()
    {
      this.workingFolderField = ExeHostEntryPointTypeWorkingFolder.Work;
      this.isExternalExecutableField = false;
    }

    public string Program
    {
      get
      {
        return this.programField;
      }
      set
      {
        this.programField = value;
      }
    }

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

    [DefaultValue(ExeHostEntryPointTypeWorkingFolder.Work)]
    public ExeHostEntryPointTypeWorkingFolder WorkingFolder
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

    public ExeHostEntryPointTypeConsoleRedirection ConsoleRedirection
    {
      get
      {
        return this.consoleRedirectionField;
      }
      set
      {
        this.consoleRedirectionField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool IsExternalExecutable
    {
      get
      {
        return this.isExternalExecutableField;
      }
      set
      {
        this.isExternalExecutableField = value;
      }
    }
  }
}
