// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServicePackageTypeDigestedCodePackage
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
