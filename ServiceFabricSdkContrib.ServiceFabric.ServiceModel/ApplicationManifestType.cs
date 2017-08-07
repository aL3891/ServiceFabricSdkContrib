// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ApplicationManifestType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  [XmlRoot("ApplicationManifest", IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ApplicationManifestType
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
    private XmlAttribute[] anyAttrField;

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
