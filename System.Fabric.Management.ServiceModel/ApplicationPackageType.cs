// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ApplicationPackageType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  [XmlRoot("ApplicationPackage", IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ApplicationPackageType
  {
    private EnvironmentType digestedEnvironmentField;
    private ApplicationPackageTypeDigestedCertificates digestedCertificatesField;
    private string applicationTypeNameField;
    private string rolloutVersionField;
    private string nameUriField;
    private string applicationIdField;
    private string contentChecksumField;

    public EnvironmentType DigestedEnvironment
    {
      get
      {
        return this.digestedEnvironmentField;
      }
      set
      {
        this.digestedEnvironmentField = value;
      }
    }

    public ApplicationPackageTypeDigestedCertificates DigestedCertificates
    {
      get
      {
        return this.digestedCertificatesField;
      }
      set
      {
        this.digestedCertificatesField = value;
      }
    }

    [XmlAttribute]
    public string ApplicationTypeName
    {
      get
      {
        return this.applicationTypeNameField;
      }
      set
      {
        this.applicationTypeNameField = value;
      }
    }

    [XmlAttribute]
    public string RolloutVersion
    {
      get
      {
        return this.rolloutVersionField;
      }
      set
      {
        this.rolloutVersionField = value;
      }
    }

    [XmlAttribute(DataType = "anyURI")]
    public string NameUri
    {
      get
      {
        return this.nameUriField;
      }
      set
      {
        this.nameUriField = value;
      }
    }

    [XmlAttribute]
    public string ApplicationId
    {
      get
      {
        return this.applicationIdField;
      }
      set
      {
        this.applicationIdField = value;
      }
    }

    [XmlAttribute]
    public string ContentChecksum
    {
      get
      {
        return this.contentChecksumField;
      }
      set
      {
        this.contentChecksumField = value;
      }
    }
  }
}
