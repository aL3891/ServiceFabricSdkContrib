// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.SecurityPrincipalsTypeUser
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
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class SecurityPrincipalsTypeUser
  {
    private SecurityPrincipalsTypeUserNTLMAuthenticationPolicy nTLMAuthenticationPolicyField;
    private object[] memberOfField;
    private string nameField;
    private SecurityPrincipalsTypeUserAccountType accountTypeField;
    private bool loadUserProfileField;
    private bool performInteractiveLogonField;
    private string accountNameField;
    private string passwordField;
    private bool passwordEncryptedField;
    private bool passwordEncryptedFieldSpecified;

    public SecurityPrincipalsTypeUser()
    {
      this.accountTypeField = SecurityPrincipalsTypeUserAccountType.LocalUser;
      this.loadUserProfileField = false;
      this.performInteractiveLogonField = false;
    }

    public SecurityPrincipalsTypeUserNTLMAuthenticationPolicy NTLMAuthenticationPolicy
    {
      get
      {
        return this.nTLMAuthenticationPolicyField;
      }
      set
      {
        this.nTLMAuthenticationPolicyField = value;
      }
    }

    [XmlArrayItem("Group", typeof (SecurityPrincipalsTypeUserGroup), IsNullable = false)]
    [XmlArrayItem("SystemGroup", typeof (SecurityPrincipalsTypeUserSystemGroup), IsNullable = false)]
    public object[] MemberOf
    {
      get
      {
        return this.memberOfField;
      }
      set
      {
        this.memberOfField = value;
      }
    }

    [XmlAttribute]
    public string Name
    {
      get
      {
        return this.nameField;
      }
      set
      {
        this.nameField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(SecurityPrincipalsTypeUserAccountType.LocalUser)]
    public SecurityPrincipalsTypeUserAccountType AccountType
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
    [DefaultValue(false)]
    public bool LoadUserProfile
    {
      get
      {
        return this.loadUserProfileField;
      }
      set
      {
        this.loadUserProfileField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool PerformInteractiveLogon
    {
      get
      {
        return this.performInteractiveLogonField;
      }
      set
      {
        this.performInteractiveLogonField = value;
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
  }
}
