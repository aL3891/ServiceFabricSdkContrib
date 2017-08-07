// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.FileStoreType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [XmlInclude(typeof (FileStoreETWType))]
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class FileStoreType
  {
    private ParameterType[] parametersField;
    private string isEnabledField;
    private string pathField;
    private string uploadIntervalInMinutesField;
    private string dataDeletionAgeInDaysField;
    private string accountTypeField;
    private string accountNameField;
    private string passwordField;
    private string passwordEncryptedField;

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
    public string Path
    {
      get
      {
        return this.pathField;
      }
      set
      {
        this.pathField = value;
      }
    }

    [XmlAttribute]
    public string UploadIntervalInMinutes
    {
      get
      {
        return this.uploadIntervalInMinutesField;
      }
      set
      {
        this.uploadIntervalInMinutesField = value;
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

    [XmlAttribute]
    public string AccountType
    {
      get
      {
        return this.accountTypeField;
      }
      set
      {
        this.accountTypeField = value;
      }
    }

    [XmlAttribute]
    public string AccountName
    {
      get
      {
        return this.accountNameField;
      }
      set
      {
        this.accountNameField = value;
      }
    }

    [XmlAttribute]
    public string Password
    {
      get
      {
        return this.passwordField;
      }
      set
      {
        this.passwordField = value;
      }
    }

    [XmlAttribute]
    public string PasswordEncrypted
    {
      get
      {
        return this.passwordEncryptedField;
      }
      set
      {
        this.passwordEncryptedField = value;
      }
    }
  }
}
