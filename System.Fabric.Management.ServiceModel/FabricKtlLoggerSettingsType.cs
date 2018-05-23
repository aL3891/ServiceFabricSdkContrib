// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.FabricKtlLoggerSettingsType
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
  public class FabricKtlLoggerSettingsType
  {
    private FabricKtlLoggerSettingsTypeSharedLogFilePath sharedLogFilePathField;
    private FabricKtlLoggerSettingsTypeSharedLogFileId sharedLogFileIdField;
    private FabricKtlLoggerSettingsTypeSharedLogFileSizeInMB sharedLogFileSizeInMBField;

    public FabricKtlLoggerSettingsTypeSharedLogFilePath SharedLogFilePath
    {
      get
      {
        return this.sharedLogFilePathField;
      }
      set
      {
        this.sharedLogFilePathField = value;
      }
    }

    public FabricKtlLoggerSettingsTypeSharedLogFileId SharedLogFileId
    {
      get
      {
        return this.sharedLogFileIdField;
      }
      set
      {
        this.sharedLogFileIdField = value;
      }
    }

    public FabricKtlLoggerSettingsTypeSharedLogFileSizeInMB SharedLogFileSizeInMB
    {
      get
      {
        return this.sharedLogFileSizeInMBField;
      }
      set
      {
        this.sharedLogFileSizeInMBField = value;
      }
    }
  }
}
