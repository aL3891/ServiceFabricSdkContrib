﻿// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServicePackageTypeDigestedResourcesDigestedEndpoint
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
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServicePackageTypeDigestedResourcesDigestedEndpoint
  {
    private EndpointType endpointField;
    private SecurityAccessPolicyType securityAccessPolicyField;
    private EndpointBindingPolicyType endpointBindingPolicyField;
    private ResourceGovernancePolicyType resourceGovernancePolicyField;

    public EndpointType Endpoint
    {
      get
      {
        return this.endpointField;
      }
      set
      {
        this.endpointField = value;
      }
    }

    public SecurityAccessPolicyType SecurityAccessPolicy
    {
      get
      {
        return this.securityAccessPolicyField;
      }
      set
      {
        this.securityAccessPolicyField = value;
      }
    }

    public EndpointBindingPolicyType EndpointBindingPolicy
    {
      get
      {
        return this.endpointBindingPolicyField;
      }
      set
      {
        this.endpointBindingPolicyField = value;
      }
    }

    public ResourceGovernancePolicyType ResourceGovernancePolicy
    {
      get
      {
        return this.resourceGovernancePolicyField;
      }
      set
      {
        this.resourceGovernancePolicyField = value;
      }
    }
  }
}