// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.SecurityPrincipalsTypeGroup
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class SecurityPrincipalsTypeGroup
  {
    private SecurityPrincipalsTypeGroupNTLMAuthenticationPolicy nTLMAuthenticationPolicyField;
    private object[] membershipField;
    private string nameField;

    public SecurityPrincipalsTypeGroupNTLMAuthenticationPolicy NTLMAuthenticationPolicy
    {
      get
      {
        return this.nTLMAuthenticationPolicyField;
      }
      set
      {
        this.nTLMAuthenticationPolicyField = value;
      }
    }

    [XmlArrayItem("DomainGroup", typeof (SecurityPrincipalsTypeGroupDomainGroup), IsNullable = false)]
    [XmlArrayItem("DomainUser", typeof (SecurityPrincipalsTypeGroupDomainUser), IsNullable = false)]
    [XmlArrayItem("SystemGroup", typeof (SecurityPrincipalsTypeGroupSystemGroup), IsNullable = false)]
    public object[] Membership
    {
      get
      {
        return this.membershipField;
      }
      set
      {
        this.membershipField = value;
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
