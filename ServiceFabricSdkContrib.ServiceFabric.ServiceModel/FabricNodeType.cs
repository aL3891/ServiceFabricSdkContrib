// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.FabricNodeType
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
  public class FabricNodeType
  {
    private string nodeNameField;
    private string iPAddressOrFQDNField;
    private bool isSeedNodeField;
    private string nodeTypeRefField;
    private string faultDomainField;
    private string upgradeDomainField;

    [XmlAttribute]
    public string NodeName
    {
      get
      {
        return this.nodeNameField;
      }
      set
      {
        this.nodeNameField = value;
      }
    }

    [XmlAttribute]
    public string IPAddressOrFQDN
    {
      get
      {
        return this.iPAddressOrFQDNField;
      }
      set
      {
        this.iPAddressOrFQDNField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool IsSeedNode
    {
      get
      {
        return this.isSeedNodeField;
      }
      set
      {
        this.isSeedNodeField = value;
      }
    }

    [XmlAttribute]
    public string NodeTypeRef
    {
      get
      {
        return this.nodeTypeRefField;
      }
      set
      {
        this.nodeTypeRefField = value;
      }
    }

    [XmlAttribute(DataType = "anyURI")]
    public string FaultDomain
    {
      get
      {
        return this.faultDomainField;
      }
      set
      {
        this.faultDomainField = value;
      }
    }

    [XmlAttribute(DataType = "anyURI")]
    public string UpgradeDomain
    {
      get
      {
        return this.upgradeDomainField;
      }
      set
      {
        this.upgradeDomainField = value;
      }
    }

    public FabricNodeType()
    {
      this.isSeedNodeField = false;
    }
  }
}
