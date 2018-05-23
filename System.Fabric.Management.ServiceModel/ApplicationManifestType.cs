// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ApplicationManifestType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  [XmlRoot("ApplicationManifest", IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ApplicationManifestType : IXmlValidator
  {
    private string descriptionField;
    private ApplicationManifestTypeParameter[] parametersField;
    private ApplicationManifestTypeServiceManifestImport[] serviceManifestImportField;
    private ServiceType[] serviceTemplatesField;
    private DefaultServicesType defaultServicesField;
    private SecurityPrincipalsType principalsField;
    private ApplicationPoliciesType policiesField;
    private DiagnosticsType diagnosticsField;
    private ApplicationManifestTypeCertificates certificatesField;
    private string applicationTypeNameField;
    private string applicationTypeVersionField;
    private string manifestIdField;
    private XmlAttribute[] anyAttrField;

    public void Validate()
    {
      if (this.ServiceManifestImport == null || this.ServiceManifestImport.Length == 0 || this.ServiceManifestImport[0] == null)
        throw new InvalidOperationException("The element 'ApplicationManifest' in namespace 'http://schemas.microsoft.com/2011/01/fabric' has incomplete content.List of possible elements expected: 'ServiceManifestImport' in namespace 'http://schemas.microsoft.com/2011/01/fabric'. ");
    }

    public ApplicationManifestType()
    {
      this.manifestIdField = "";
    }

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

    [XmlArrayItem("Parameter", IsNullable = false)]
    public ApplicationManifestTypeParameter[] Parameters
    {
      get
      {
        return this.parametersField;
      }
      set
      {
        this.parametersField = value;
      }
    }

    [XmlElement("ServiceManifestImport")]
    public ApplicationManifestTypeServiceManifestImport[] ServiceManifestImport
    {
      get
      {
        return this.serviceManifestImportField;
      }
      set
      {
        this.serviceManifestImportField = value;
      }
    }

    [XmlArrayItem("StatefulService", typeof (StatefulServiceType), IsNullable = false)]
    [XmlArrayItem("StatefulServiceGroup", typeof (StatefulServiceGroupType), IsNullable = false)]
    [XmlArrayItem("StatelessService", typeof (StatelessServiceType), IsNullable = false)]
    [XmlArrayItem("StatelessServiceGroup", typeof (StatelessServiceGroupType), IsNullable = false)]
    public ServiceType[] ServiceTemplates
    {
      get
      {
        return this.serviceTemplatesField;
      }
      set
      {
        this.serviceTemplatesField = value;
      }
    }

    public DefaultServicesType DefaultServices
    {
      get
      {
        return this.defaultServicesField;
      }
      set
      {
        this.defaultServicesField = value;
      }
    }

    public SecurityPrincipalsType Principals
    {
      get
      {
        return this.principalsField;
      }
      set
      {
        this.principalsField = value;
      }
    }

    public ApplicationPoliciesType Policies
    {
      get
      {
        return this.policiesField;
      }
      set
      {
        this.policiesField = value;
      }
    }

    public DiagnosticsType Diagnostics
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

    public ApplicationManifestTypeCertificates Certificates
    {
      get
      {
        return this.certificatesField;
      }
      set
      {
        this.certificatesField = value;
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
    public string ApplicationTypeVersion
    {
      get
      {
        return this.applicationTypeVersionField;
      }
      set
      {
        this.applicationTypeVersionField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("")]
    public string ManifestId
    {
      get
      {
        return this.manifestIdField;
      }
      set
      {
        this.manifestIdField = value;
      }
    }

    [XmlAnyAttribute]
    public XmlAttribute[] AnyAttr
    {
      get
      {
        return this.anyAttrField;
      }
      set
      {
        this.anyAttrField = value;
      }
    }
  }
}
