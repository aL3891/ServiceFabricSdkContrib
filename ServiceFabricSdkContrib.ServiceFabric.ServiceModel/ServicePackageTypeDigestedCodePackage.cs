// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ServicePackageTypeDigestedCodePackage
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
  public class ServicePackageTypeDigestedCodePackage
  {
    private CodePackageType codePackageField;
    private RunAsPolicyType[] runAsPolicyField;
    private DebugParametersType debugParametersField;
    private ContainerHostPoliciesType containerHostPoliciesField;
    private ResourceGovernancePolicyType resourceGovernancePolicyField;
    private string rolloutVersionField;
    private string contentChecksumField;
    private bool isSharedField;
    private bool isSharedFieldSpecified;

    public CodePackageType CodePackage
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

    [XmlElement("RunAsPolicy")]
    public RunAsPolicyType[] RunAsPolicy
    {
      get
      {
        return this.runAsPolicyField;
      }
      set
      {
        this.runAsPolicyField = value;
      }
    }

    public DebugParametersType DebugParameters
    {
      get
      {
        return this.debugParametersField;
      }
      set
      {
        this.debugParametersField = value;
      }
    }

    public ContainerHostPoliciesType ContainerHostPolicies
    {
      get
      {
        return this.containerHostPoliciesField;
      }
      set
      {
        this.containerHostPoliciesField = value;
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

    [XmlAttribute]
    public bool IsShared
    {
      get
      {
        return this.isSharedField;
      }
      set
      {
        this.isSharedField = value;
      }
    }

    [XmlIgnore]
    public bool IsSharedSpecified
    {
      get
      {
        return this.isSharedFieldSpecified;
      }
      set
      {
        this.isSharedFieldSpecified = value;
      }
    }
  }
}
