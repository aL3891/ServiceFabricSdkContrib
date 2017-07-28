// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ApplicationPackageType
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
