// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.FabricCertificateType
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
  public class FabricCertificateType
  {
    private string x509StoreNameField;
    private FabricCertificateTypeX509FindType x509FindTypeField;
    private string x509FindValueField;
    private string x509FindValueSecondaryField;
    private string nameField;

    public FabricCertificateType()
    {
      this.x509StoreNameField = "My";
      this.x509FindTypeField = FabricCertificateTypeX509FindType.FindByThumbprint;
      this.x509FindValueSecondaryField = "";
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
    [DefaultValue(FabricCertificateTypeX509FindType.FindByThumbprint)]
    public FabricCertificateTypeX509FindType X509FindType
    {
      get
      {
        return this.x509FindTypeField;
      }
      set
      {
        this.x509FindTypeField = value;
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
    [DefaultValue("")]
    public string X509FindValueSecondary
    {
      get
      {
        return this.x509FindValueSecondaryField;
      }
      set
      {
        this.x509FindValueSecondaryField = value;
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
