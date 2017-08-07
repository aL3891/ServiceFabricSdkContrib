// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.SecurityPrincipalsTypeUser
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
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

    public SecurityPrincipalsTypeUser()
    {
      this.accountTypeField = SecurityPrincipalsTypeUserAccountType.LocalUser;
      this.loadUserProfileField = false;
      this.performInteractiveLogonField = false;
    }
  }
}
