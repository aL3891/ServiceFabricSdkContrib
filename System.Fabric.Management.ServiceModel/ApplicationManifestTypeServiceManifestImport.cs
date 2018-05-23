// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ApplicationManifestTypeServiceManifestImport
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
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ApplicationManifestTypeServiceManifestImport
  {
    private ServiceManifestRefType serviceManifestRefField;
    private ConfigOverrideType[] configOverridesField;
    private ResourceOverridesType resourceOverridesField;
    private EnvironmentOverridesType[] environmentOverridesField;
    private object[] policiesField;

    public ServiceManifestRefType ServiceManifestRef
    {
      get
      {
        return this.serviceManifestRefField;
      }
      set
      {
        this.serviceManifestRefField = value;
      }
    }

    [XmlArrayItem("ConfigOverride", IsNullable = false)]
    public ConfigOverrideType[] ConfigOverrides
    {
      get
      {
        return this.configOverridesField;
      }
      set
      {
        this.configOverridesField = value;
      }
    }

    public ResourceOverridesType ResourceOverrides
    {
      get
      {
        return this.resourceOverridesField;
      }
      set
      {
        this.resourceOverridesField = value;
      }
    }

    [XmlElement("EnvironmentOverrides")]
    public EnvironmentOverridesType[] EnvironmentOverrides
    {
      get
      {
        return this.environmentOverridesField;
      }
      set
      {
        this.environmentOverridesField = value;
      }
    }

    [XmlArrayItem("ContainerHostPolicies", typeof (ContainerHostPoliciesType), IsNullable = false)]
    [XmlArrayItem("EndpointBindingPolicy", typeof (EndpointBindingPolicyType), IsNullable = false)]
    [XmlArrayItem("PackageSharingPolicy", typeof (PackageSharingPolicyType), IsNullable = false)]
    [XmlArrayItem("ResourceGovernancePolicy", typeof (ResourceGovernancePolicyType), IsNullable = false)]
    [XmlArrayItem("RunAsPolicy", typeof (RunAsPolicyType), IsNullable = false)]
    [XmlArrayItem("SecurityAccessPolicy", typeof (SecurityAccessPolicyType), IsNullable = false)]
    [XmlArrayItem("ServicePackageResourceGovernancePolicy", typeof (ServicePackageResourceGovernancePolicyType), IsNullable = false)]
    public object[] Policies
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
  }
}
