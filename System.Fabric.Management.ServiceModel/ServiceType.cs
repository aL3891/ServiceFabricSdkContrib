// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
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
    private ScalingPolicyType[] serviceScalingPoliciesField;
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

    [XmlArrayItem("ScalingPolicy", IsNullable = false)]
    public ScalingPolicyType[] ServiceScalingPolicies
    {
      get
      {
        return this.serviceScalingPoliciesField;
      }
      set
      {
        this.serviceScalingPoliciesField = value;
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
