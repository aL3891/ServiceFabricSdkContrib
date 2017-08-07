// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ServiceTypeType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [XmlInclude(typeof (StatelessServiceTypeType))]
  [XmlInclude(typeof (StatefulServiceTypeType))]
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServiceTypeType
  {
    private LoadMetricType[] loadMetricsField;
    private string placementConstraintsField;
    private ServiceTypeTypeServicePlacementPolicy[] servicePlacementPoliciesField;
    private ExtensionsTypeExtension[] extensionsField;
    private string serviceTypeNameField;

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

    [XmlArrayItem("ServicePlacementPolicy", IsNullable = false)]
    public ServiceTypeTypeServicePlacementPolicy[] ServicePlacementPolicies
    {
      get
      {
        return this.servicePlacementPoliciesField;
      }
      set
      {
        this.servicePlacementPoliciesField = value;
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
    public string ServiceTypeName
    {
      get
      {
        return this.serviceTypeNameField;
      }
      set
      {
        this.serviceTypeNameField = value;
      }
    }
  }
}
