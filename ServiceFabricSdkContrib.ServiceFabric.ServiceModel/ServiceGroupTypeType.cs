// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ServiceGroupTypeType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [XmlInclude(typeof (StatelessServiceGroupTypeType))]
  [XmlInclude(typeof (StatefulServiceGroupTypeType))]
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServiceGroupTypeType
  {
    private LoadMetricType[] loadMetricsField;
    private string placementConstraintsField;
    private ServiceGroupTypeMember[] serviceGroupMembersField;
    private ExtensionsTypeExtension[] extensionsField;
    private string serviceGroupTypeNameField;
    private bool useImplicitFactoryField;
    private bool useImplicitFactoryFieldSpecified;

    [XmlArrayItem("LoadMetric", IsNullable = false)]
    public LoadMetricType[] LoadMetrics
    {
      get
      {
        return this.loadMetricsField;
      }
      set
      {
        this.loadMetricsField = value;
      }
    }

    public string PlacementConstraints
    {
      get
      {
        return this.placementConstraintsField;
      }
      set
      {
        this.placementConstraintsField = value;
      }
    }

    [XmlArrayItem("ServiceGroupTypeMember", IsNullable = false)]
    public ServiceGroupTypeMember[] ServiceGroupMembers
    {
      get
      {
        return this.serviceGroupMembersField;
      }
      set
      {
        this.serviceGroupMembersField = value;
      }
    }

    [XmlArrayItem("Extension", IsNullable = false)]
    public ExtensionsTypeExtension[] Extensions
    {
      get
      {
        return this.extensionsField;
      }
      set
      {
        this.extensionsField = value;
      }
    }

    [XmlAttribute]
    public string ServiceGroupTypeName
    {
      get
      {
        return this.serviceGroupTypeNameField;
      }
      set
      {
        this.serviceGroupTypeNameField = value;
      }
    }

    [XmlAttribute]
    public bool UseImplicitFactory
    {
      get
      {
        return this.useImplicitFactoryField;
      }
      set
      {
        this.useImplicitFactoryField = value;
      }
    }

    [XmlIgnore]
    public bool UseImplicitFactorySpecified
    {
      get
      {
        return this.useImplicitFactoryFieldSpecified;
      }
      set
      {
        this.useImplicitFactoryFieldSpecified = value;
      }
    }
  }
}
