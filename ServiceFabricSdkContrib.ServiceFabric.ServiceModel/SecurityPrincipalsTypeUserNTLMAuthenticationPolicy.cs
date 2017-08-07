// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.SecurityPrincipalsTypeUserNTLMAuthenticationPolicy
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
  public class SecurityPrincipalsTypeUserNTLMAuthenticationPolicy
  {
    private bool isEnabledField;
    private string passwordSecretField;
    private bool passwordSecretEncryptedField;
    private SecurityPrincipalsTypeUserNTLMAuthenticationPolicyX509StoreLocation x509StoreLocationField;
    private string x509StoreNameField;
    private string x509ThumbprintField;

    [XmlAttribute]
    [DefaultValue(true)]
    public bool IsEnabled
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
    public string PasswordSecret
    {
      get
      {
        return this.passwordSecretField;
      }
      set
      {
        this.passwordSecretField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool PasswordSecretEncrypted
    {
      get
      {
        return this.passwordSecretEncryptedField;
      }
      set
      {
        this.passwordSecretEncryptedField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(SecurityPrincipalsTypeUserNTLMAuthenticationPolicyX509StoreLocation.LocalMachine)]
    public SecurityPrincipalsTypeUserNTLMAuthenticationPolicyX509StoreLocation X509StoreLocation
    {
      get
      {
        return this.x509StoreLocationField;
      }
      set
      {
        this.x509StoreLocationField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("My")]
    public string X509StoreName
    {
      get
      {
        return this.x509StoreNameField;
      }
      set
      {
        this.x509StoreNameField = value;
      }
    }

    [XmlAttribute]
    public string X509Thumbprint
    {
      get
      {
        return this.x509ThumbprintField;
      }
      set
      {
        this.x509ThumbprintField = value;
      }
    }

    public SecurityPrincipalsTypeUserNTLMAuthenticationPolicy()
    {
      this.isEnabledField = true;
      this.passwordSecretEncryptedField = false;
      this.x509StoreLocationField = SecurityPrincipalsTypeUserNTLMAuthenticationPolicyX509StoreLocation.LocalMachine;
      this.x509StoreNameField = "My";
    }
  }
}
