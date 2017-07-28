// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ApplicationPoliciesType
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
  public class ApplicationPoliciesType
  {
    private ApplicationPoliciesTypeLogCollectionPolicies logCollectionPoliciesField;
    private ApplicationPoliciesTypeDefaultRunAsPolicy defaultRunAsPolicyField;
    private ApplicationHealthPolicyType healthPolicyField;
    private ApplicationPoliciesTypeSecurityAccessPolicies securityAccessPoliciesField;

    public ApplicationPoliciesTypeLogCollectionPolicies LogCollectionPolicies
    {
      get
      {
        return this.logCollectionPoliciesField;
      }
      set
      {
        this.logCollectionPoliciesField = value;
      }
    }

    public ApplicationPoliciesTypeDefaultRunAsPolicy DefaultRunAsPolicy
    {
      get
      {
        return this.defaultRunAsPolicyField;
      }
      set
      {
        this.defaultRunAsPolicyField = value;
      }
    }

    public ApplicationHealthPolicyType HealthPolicy
    {
      get
      {
        return this.healthPolicyField;
      }
      set
      {
        this.healthPolicyField = value;
      }
    }

    public ApplicationPoliciesTypeSecurityAccessPolicies SecurityAccessPolicies
    {
      get
      {
        return this.securityAccessPoliciesField;
      }
      set
      {
        this.securityAccessPoliciesField = value;
      }
    }
  }
}
