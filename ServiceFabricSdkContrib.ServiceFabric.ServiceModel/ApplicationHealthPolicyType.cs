// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ApplicationHealthPolicyType
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ApplicationHealthPolicyType
  {
    private ServiceTypeHealthPolicyType defaultServiceTypeHealthPolicyField;
    private ApplicationHealthPolicyTypeServiceTypeHealthPolicy[] serviceTypeHealthPolicyField;
    private string considerWarningAsErrorField;
    private string maxPercentUnhealthyDeployedApplicationsField;

    public ServiceTypeHealthPolicyType DefaultServiceTypeHealthPolicy
    {
      get
      {
        return this.defaultServiceTypeHealthPolicyField;
      }
      set
      {
        this.defaultServiceTypeHealthPolicyField = value;
      }
    }

    [XmlElement("ServiceTypeHealthPolicy")]
    public ApplicationHealthPolicyTypeServiceTypeHealthPolicy[] ServiceTypeHealthPolicy
    {
      get
      {
        return this.serviceTypeHealthPolicyField;
      }
      set
      {
        this.serviceTypeHealthPolicyField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("false")]
    public string ConsiderWarningAsError
    {
      get
      {
        return this.considerWarningAsErrorField;
      }
      set
      {
        this.considerWarningAsErrorField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MaxPercentUnhealthyDeployedApplications
    {
      get
      {
        return this.maxPercentUnhealthyDeployedApplicationsField;
      }
      set
      {
        this.maxPercentUnhealthyDeployedApplicationsField = value;
      }
    }

    public ApplicationHealthPolicyType()
    {
      this.considerWarningAsErrorField = "false";
      this.maxPercentUnhealthyDeployedApplicationsField = "0";
    }
  }
}
