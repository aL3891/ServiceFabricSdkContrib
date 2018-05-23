// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceManifestType
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
  [XmlRoot("ServiceManifest", IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServiceManifestType : IXmlValidator
  {
    private string descriptionField;
    private object[] serviceTypesField;
    private CodePackageType[] codePackageField;
    private ConfigPackageType[] configPackageField;
    private DataPackageType[] dataPackageField;
    private ResourcesType resourcesField;
    private ServiceDiagnosticsType diagnosticsField;
    private string manifestIdField;
    private string nameField;
    private string versionField;
    private XmlAttribute[] anyAttrField;

    public void Validate()
    {
      if (this.ServiceTypes == null || this.ServiceTypes.Length == 0 || this.ServiceTypes[0] == null)
        throw new InvalidOperationException("The element 'ServiceManifest' in namespace 'http://schemas.microsoft.com/2011/01/fabric' has incomplete content.List of possible elements expected: 'ServiceTypes' in namespace 'http://schemas.microsoft.com/2011/01/fabric'. ");
      if (this.CodePackage == null || this.CodePackage.Length == 0 || this.CodePackage[0] == null)
        throw new InvalidOperationException("The element 'ServiceManifest' in namespace 'http://schemas.microsoft.com/2011/01/fabric' has incomplete content.List of possible elements expected: 'CodePackage' in namespace 'http://schemas.microsoft.com/2011/01/fabric'. ");
    }

    public ServiceManifestType()
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

    [XmlArrayItem("StatefulServiceGroupType", typeof (StatefulServiceGroupTypeType), IsNullable = false)]
    [XmlArrayItem("StatefulServiceType", typeof (StatefulServiceTypeType), IsNullable = false)]
    [XmlArrayItem("StatelessServiceGroupType", typeof (StatelessServiceGroupTypeType), IsNullable = false)]
    [XmlArrayItem("StatelessServiceType", typeof (StatelessServiceTypeType), IsNullable = false)]
    public object[] ServiceTypes
    {
      get
      {
        return this.serviceTypesField;
      }
      set
      {
        this.serviceTypesField = value;
      }
    }

    [XmlElement("CodePackage")]
    public CodePackageType[] CodePackage
    {
      get
      {
        return this.codePackageField;
      }
      set
      {
        this.codePackageField = value;
      }
    }

    [XmlElement("ConfigPackage")]
    public ConfigPackageType[] ConfigPackage
    {
      get
      {
        return this.configPackageField;
      }
      set
      {
        this.configPackageField = value;
      }
    }

    [XmlElement("DataPackage")]
    public DataPackageType[] DataPackage
    {
      get
      {
        return this.dataPackageField;
      }
      set
      {
        this.dataPackageField = value;
      }
    }

    public ResourcesType Resources
    {
      get
      {
        return this.resourcesField;
      }
      set
      {
        this.resourcesField = value;
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
    public string Version
    {
      get
      {
        return this.versionField;
      }
      set
      {
        this.versionField = value;
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
