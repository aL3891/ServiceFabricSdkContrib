﻿// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.EvictionPolicy
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  [XmlRoot(IsNullable = false, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class EvictionPolicy
  {
    private ServiceTypeExtensionPolicyPropertiesTypeProperty[] propertiesField;
    private string nameField;
    private string providerField;

    [XmlArrayItem("Property", IsNullable = false)]
    public ServiceTypeExtensionPolicyPropertiesTypeProperty[] Properties
    {
      get
      {
        return this.propertiesField;
      }
      set
      {
        this.propertiesField = value;
      }
    }

    [XmlAttribute]
    public string Name
    {
      get
      {
        return this.nameField;
      }
      set
      {
        this.nameField = value;
      }
    }

    [XmlAttribute]
    public string Provider
    {
      get
      {
        return this.providerField;
      }
      set
      {
        this.providerField = value;
      }
    }
  }
}
