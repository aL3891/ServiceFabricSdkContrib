// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.SecurityPrincipalsTypeUserNTLMAuthenticationPolicy
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
  public class SecurityPrincipalsTypeUserNTLMAuthenticationPolicy
  {
    private bool isEnabledField;
    private string passwordSecretField;
    private bool passwordSecretEncryptedField;
    private SecurityPrincipalsTypeUserNTLMAuthenticationPolicyX509StoreLocation x509StoreLocationField;
    private string x509StoreNameField;
    private string x509ThumbprintField;

    public SecurityPrincipalsTypeUserNTLMAuthenticationPolicy()
    {
      this.isEnabledField = true;
      this.passwordSecretEncryptedField = false;
      this.x509StoreLocationField = SecurityPrincipalsTypeUserNTLMAuthenticationPolicyX509StoreLocation.LocalMachine;
      this.x509StoreNameField = "My";
    }

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
  }
}
