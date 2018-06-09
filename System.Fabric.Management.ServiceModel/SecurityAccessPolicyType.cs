// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.SecurityAccessPolicyType
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
  public class SecurityAccessPolicyType
  {
    private string resourceRefField;
    private string principalRefField;
    private SecurityAccessPolicyTypeGrantRights grantRightsField;
    private SecurityAccessPolicyTypeResourceType resourceTypeField;

    public SecurityAccessPolicyType()
    {
      this.grantRightsField = SecurityAccessPolicyTypeGrantRights.Read;
      this.resourceTypeField = SecurityAccessPolicyTypeResourceType.Endpoint;
    }

    [XmlAttribute]
    public string ResourceRef
    {
      get
      {
        return this.resourceRefField;
      }
      set
      {
        this.resourceRefField = value;
      }
    }

    [XmlAttribute]
    public string PrincipalRef
    {
      get
      {
        return this.principalRefField;
      }
      set
      {
        this.principalRefField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(SecurityAccessPolicyTypeGrantRights.Read)]
    public SecurityAccessPolicyTypeGrantRights GrantRights
    {
      get
      {
        return this.grantRightsField;
      }
      set
      {
        this.grantRightsField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(SecurityAccessPolicyTypeResourceType.Endpoint)]
    public SecurityAccessPolicyTypeResourceType ResourceType
    {
      get
      {
        return this.resourceTypeField;
      }
      set
      {
        this.resourceTypeField = value;
      }
    }
  }
}
