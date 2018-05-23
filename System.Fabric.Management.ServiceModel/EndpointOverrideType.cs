// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.EndpointOverrideType
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class EndpointOverrideType
  {
    private string nameField;
    private string portField;
    private string protocolField;
    private string typeField;
    private string uriSchemeField;
    private string pathSuffixField;

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
    public string Port
    {
      get
      {
        return this.portField;
      }
      set
      {
        this.portField = value;
      }
    }

    [XmlAttribute]
    public string Protocol
    {
      get
      {
        return this.protocolField;
      }
      set
      {
        this.protocolField = value;
      }
    }

    [XmlAttribute]
    public string Type
    {
      get
      {
        return this.typeField;
      }
      set
      {
        this.typeField = value;
      }
    }

    [XmlAttribute]
    public string UriScheme
    {
      get
      {
        return this.uriSchemeField;
      }
      set
      {
        this.uriSchemeField = value;
      }
    }

    [XmlAttribute]
    public string PathSuffix
    {
      get
      {
        return this.pathSuffixField;
      }
      set
      {
        this.pathSuffixField = value;
      }
    }
  }
}
