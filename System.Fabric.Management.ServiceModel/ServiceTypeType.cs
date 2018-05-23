// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceTypeType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
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
