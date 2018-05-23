// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.DiagnosticsType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class DiagnosticsType
  {
    private DiagnosticsTypeCrashDumpSource crashDumpSourceField;
    private DiagnosticsTypeETWSource eTWSourceField;
    private DiagnosticsTypeFolderSource[] folderSourceField;

    public DiagnosticsTypeCrashDumpSource CrashDumpSource
    {
      get
      {
        return this.crashDumpSourceField;
      }
      set
      {
        this.crashDumpSourceField = value;
      }
    }

    public DiagnosticsTypeETWSource ETWSource
    {
      get
      {
        return this.eTWSourceField;
      }
      set
      {
        this.eTWSourceField = value;
      }
    }

    [XmlElement("FolderSource")]
    public DiagnosticsTypeFolderSource[] FolderSource
    {
      get
      {
        return this.folderSourceField;
      }
      set
      {
        this.folderSourceField = value;
      }
    }
  }
}
