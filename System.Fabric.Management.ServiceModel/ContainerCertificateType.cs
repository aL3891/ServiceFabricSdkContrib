// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ContainerCertificateType
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
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ContainerCertificateType
  {
    private string x509StoreNameField;
    private string x509FindValueField;
    private string dataPackageRefField;
    private string dataPackageVersionField;
    private string relativePathField;
    private string passwordField;
    private bool isPasswordEncryptedField;
    private string nameField;

    public ContainerCertificateType()
    {
      this.x509StoreNameField = "My";
      this.isPasswordEncryptedField = false;
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
    public string X509FindValue
    {
      get
      {
        return this.x509FindValueField;
      }
      set
      {
        this.x509FindValueField = value;
      }
    }

    [XmlAttribute]
    public string DataPackageRef
    {
      get
      {
        return this.dataPackageRefField;
      }
      set
      {
        this.dataPackageRefField = value;
      }
    }

    [XmlAttribute]
    public string DataPackageVersion
    {
      get
      {
        return this.dataPackageVersionField;
      }
      set
      {
        this.dataPackageVersionField = value;
      }
    }

    [XmlAttribute]
    public string RelativePath
    {
      get
      {
        return this.relativePathField;
      }
      set
      {
        this.relativePathField = value;
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
    [DefaultValue(false)]
    public bool IsPasswordEncrypted
    {
      get
      {
        return this.isPasswordEncryptedField;
      }
      set
      {
        this.isPasswordEncryptedField = value;
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
  }
}
