// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.EndpointType
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace System.Fabric.Management.ServiceModel
{
  [GeneratedCode("xsd", "4.0.30319.17929")]
  [DebuggerStepThrough]
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class EndpointType
  {
    private string nameField;
    private EndpointTypeProtocol protocolField;
    private EndpointTypeType typeField;
    private string certificateRefField;
    private int portField;
    private bool portFieldSpecified;
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
    [DefaultValue(EndpointTypeProtocol.tcp)]
    public EndpointTypeProtocol Protocol
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
    [DefaultValue(EndpointTypeType.Internal)]
    public EndpointTypeType Type
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
    public string CertificateRef
    {
      get
      {
        return this.certificateRefField;
      }
      set
      {
        this.certificateRefField = value;
      }
    }

    [XmlAttribute]
    public int Port
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

    [XmlIgnore]
    public bool PortSpecified
    {
      get
      {
        return this.portFieldSpecified;
      }
      set
      {
        this.portFieldSpecified = value;
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

    public EndpointType()
    {
      this.protocolField = EndpointTypeProtocol.tcp;
      this.typeField = EndpointTypeType.Internal;
    }
  }
}
