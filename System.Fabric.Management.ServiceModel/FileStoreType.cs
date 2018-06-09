// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.FileStoreType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
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
