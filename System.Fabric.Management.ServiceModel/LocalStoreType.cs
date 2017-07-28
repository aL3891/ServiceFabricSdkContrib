// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.LocalStoreType
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [XmlInclude(typeof (LocalStoreETWType))]
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class LocalStoreType
  {
    private ParameterType[] parametersField;
    private string isEnabledField;
    private string relativeFolderPathField;
    private string dataDeletionAgeInDaysField;

    [XmlArrayItem("Parameter", IsNullable = false)]
    public ParameterType[] Parameters
    {
      get
      {
        return this.parametersField;
      }
      set
      {
        this.parametersField = value;
      }
    }

    [XmlAttribute]
    public string IsEnabled
    {
      get
      {
        return this.isEnabledField;
      }
      set
      {
        this.isEnabledField = value;
      }
    }

    [XmlAttribute]
    public string RelativeFolderPath
    {
      get
      {
        return this.relativeFolderPathField;
      }
      set
      {
        this.relativeFolderPathField = value;
      }
    }

    [XmlAttribute]
    public string DataDeletionAgeInDays
    {
      get
      {
        return this.dataDeletionAgeInDaysField;
      }
      set
      {
        this.dataDeletionAgeInDaysField = value;
      }
    }
  }
}
