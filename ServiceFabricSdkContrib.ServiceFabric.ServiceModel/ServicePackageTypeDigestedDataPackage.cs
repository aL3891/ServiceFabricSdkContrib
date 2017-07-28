// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServicePackageTypeDigestedDataPackage
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
  public class ServicePackageTypeDigestedDataPackage
  {
    private DataPackageType dataPackageField;
    private DebugParametersType debugParametersField;
    private string rolloutVersionField;
    private string contentChecksumField;
    private bool isSharedField;
    private bool isSharedFieldSpecified;

    public DataPackageType DataPackage
    {
      get
      {
        return this.dataPackageField;
      }
      set
      {
        this.dataPackageField = value;
      }
    }

    public DebugParametersType DebugParameters
    {
      get
      {
        return this.debugParametersField;
      }
      set
      {
        this.debugParametersField = value;
      }
    }

    [XmlAttribute]
    public string RolloutVersion
    {
      get
      {
        return this.rolloutVersionField;
      }
      set
      {
        this.rolloutVersionField = value;
      }
    }

    [XmlAttribute]
    public string ContentChecksum
    {
      get
      {
        return this.contentChecksumField;
      }
      set
      {
        this.contentChecksumField = value;
      }
    }

    [XmlAttribute]
    public bool IsShared
    {
      get
      {
        return this.isSharedField;
      }
      set
      {
        this.isSharedField = value;
      }
    }

    [XmlIgnore]
    public bool IsSharedSpecified
    {
      get
      {
        return this.isSharedFieldSpecified;
      }
      set
      {
        this.isSharedFieldSpecified = value;
      }
    }
  }
}
