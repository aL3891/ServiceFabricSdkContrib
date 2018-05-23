// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServiceGroupTypeType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
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
