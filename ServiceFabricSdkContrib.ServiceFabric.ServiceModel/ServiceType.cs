// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.ServiceType
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  [XmlInclude(typeof (StatefulServiceType))]
  [XmlInclude(typeof (StatefulServiceGroupType))]
  [XmlInclude(typeof (StatelessServiceType))]
  [XmlInclude(typeof (StatelessServiceGroupType))]
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ServiceType
  {
    private ServiceTypeSingletonPartition singletonPartitionField;
    private ServiceTypeUniformInt64Partition uniformInt64PartitionField;
    private ServiceTypeNamedPartition namedPartitionField;
    private LoadMetricType[] loadMetricsField;
    private string placementConstraintsField;
    private ServiceTypeServiceCorrelation[] serviceCorrelationsField;
    private ServiceTypeServicePlacementPolicy[] servicePlacementPoliciesField;
    private string serviceTypeNameField;
    private ServiceTypeDefaultMoveCost defaultMoveCostField;
    private bool defaultMoveCostFieldSpecified;

    public ServiceTypeSingletonPartition SingletonPartition
    {
      get
      {
        return this.singletonPartitionField;
      }
      set
      {
        this.singletonPartitionField = value;
      }
    }

    public ServiceTypeUniformInt64Partition UniformInt64Partition
    {
      get
      {
        return this.uniformInt64PartitionField;
      }
      set
      {
        this.uniformInt64PartitionField = value;
      }
    }

    public ServiceTypeNamedPartition NamedPartition
    {
      get
      {
        return this.namedPartitionField;
      }
      set
      {
        this.namedPartitionField = value;
      }
    }

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

    [XmlArrayItem("ServiceCorrelation", IsNullable = false)]
    public ServiceTypeServiceCorrelation[] ServiceCorrelations
    {
      get
      {
        return this.serviceCorrelationsField;
      }
      set
      {
        this.serviceCorrelationsField = value;
      }
    }

    [XmlArrayItem("ServicePlacementPolicy", IsNullable = false)]
    public ServiceTypeServicePlacementPolicy[] ServicePlacementPolicies
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

    [XmlAttribute]
    public ServiceTypeDefaultMoveCost DefaultMoveCost
    {
      get
      {
        return this.defaultMoveCostField;
      }
      set
      {
        this.defaultMoveCostField = value;
      }
    }

    [XmlIgnore]
    public bool DefaultMoveCostSpecified
    {
      get
      {
        return this.defaultMoveCostFieldSpecified;
      }
      set
      {
        this.defaultMoveCostFieldSpecified = value;
      }
    }
  }
}
