// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.RepositoryCredentialsType
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class RepositoryCredentialsType
  {
    private string accountNameField;
    private string passwordField;
    private bool passwordEncryptedField;
    private bool passwordEncryptedFieldSpecified;
    private string emailField;

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
    public bool PasswordEncrypted
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

    [XmlIgnore]
    public bool PasswordEncryptedSpecified
    {
      get
      {
        return this.passwordEncryptedFieldSpecified;
      }
      set
      {
        this.passwordEncryptedFieldSpecified = value;
      }
    }

    [XmlAttribute]
    public string Email
    {
      get
      {
        return this.emailField;
      }
      set
      {
        this.emailField = value;
      }
    }
  }
}
