// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.PackageSharingPolicyType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class PackageSharingPolicyType
  {
    private string packageRefField;
    private PackageSharingPolicyTypeScope scopeField;

    [XmlAttribute]
    public string PackageRef
    {
      get
      {
        return this.packageRefField;
      }
      set
      {
        this.packageRefField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(PackageSharingPolicyTypeScope.None)]
    public PackageSharingPolicyTypeScope Scope
    {
      get
      {
        return this.scopeField;
      }
      set
      {
        this.scopeField = value;
      }
    }

    public PackageSharingPolicyType()
    {
      this.scopeField = PackageSharingPolicyTypeScope.None;
    }
  }
}
