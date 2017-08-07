// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.FabricKtlLoggerSettingsType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
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
