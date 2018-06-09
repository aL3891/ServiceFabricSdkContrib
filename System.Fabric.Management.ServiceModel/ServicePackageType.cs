// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServicePackageType
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
  [XmlRoot("ServicePackage", IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServicePackageType
  {
    private string descriptionField;
    private ServicePackageResourceGovernancePolicyType servicePackageResourceGovernancePolicyField;
    private ServicePackageTypeDigestedServiceTypes digestedServiceTypesField;
    private ServicePackageTypeDigestedCodePackage[] digestedCodePackageField;
    private ServicePackageTypeDigestedConfigPackage[] digestedConfigPackageField;
    private ServicePackageTypeDigestedDataPackage[] digestedDataPackageField;
    private ServicePackageTypeDigestedResources digestedResourcesField;
    private ServiceDiagnosticsType diagnosticsField;
    private string nameField;
    private string manifestVersionField;
    private string rolloutVersionField;
    private string manifestChecksumField;
    private string contentChecksumField;

    public string Description
    {
      get
      {
        return this.descriptionField;
      }
      set
      {
        this.descriptionField = value;
      }
    }

    public ServicePackageResourceGovernancePolicyType ServicePackageResourceGovernancePolicy
    {
      get
      {
        return this.servicePackageResourceGovernancePolicyField;
      }
      set
      {
        this.servicePackageResourceGovernancePolicyField = value;
      }
    }

    public ServicePackageTypeDigestedServiceTypes DigestedServiceTypes
    {
      get
      {
        return this.digestedServiceTypesField;
      }
      set
      {
        this.digestedServiceTypesField = value;
      }
    }

    [XmlElement("DigestedCodePackage")]
    public ServicePackageTypeDigestedCodePackage[] DigestedCodePackage
    {
      get
      {
        return this.digestedCodePackageField;
      }
      set
      {
        this.digestedCodePackageField = value;
      }
    }

    [XmlElement("DigestedConfigPackage")]
    public ServicePackageTypeDigestedConfigPackage[] DigestedConfigPackage
    {
      get
      {
        return this.digestedConfigPackageField;
      }
      set
      {
        this.digestedConfigPackageField = value;
      }
    }

    [XmlElement("DigestedDataPackage")]
    public ServicePackageTypeDigestedDataPackage[] DigestedDataPackage
    {
      get
      {
        return this.digestedDataPackageField;
      }
      set
      {
        this.digestedDataPackageField = value;
      }
    }

    public ServicePackageTypeDigestedResources DigestedResources
    {
      get
      {
        return this.digestedResourcesField;
      }
      set
      {
        this.digestedResourcesField = value;
      }
    }

    public ServiceDiagnosticsType Diagnostics
    {
      get
      {
        return this.diagnosticsField;
      }
      set
      {
        this.diagnosticsField = value;
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
    public string ManifestVersion
    {
      get
      {
        return this.manifestVersionField;
      }
      set
      {
        this.manifestVersionField = value;
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

    [XmlAttribute]
    public string ManifestChecksum
    {
      get
      {
        return this.manifestChecksumField;
      }
      set
      {
        this.manifestChecksumField = value;
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
