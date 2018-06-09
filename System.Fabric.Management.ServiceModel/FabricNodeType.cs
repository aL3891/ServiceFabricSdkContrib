// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.FabricNodeType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
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

    public FabricNodeType()
    {
      this.isSeedNodeField = false;
    }

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
  }
}
