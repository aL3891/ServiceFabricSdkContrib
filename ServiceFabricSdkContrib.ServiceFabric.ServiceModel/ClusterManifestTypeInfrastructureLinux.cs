﻿// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ClusterManifestTypeInfrastructureLinux
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
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class ClusterManifestTypeInfrastructureLinux : LinuxInfrastructureType
  {
    private bool isScaleMinField;

    [XmlAttribute]
    [DefaultValue(false)]
    public bool IsScaleMin
    {
      get
      {
        return this.isScaleMinField;
      }
      set
      {
        this.isScaleMinField = value;
      }
    }

    public ClusterManifestTypeInfrastructureLinux()
    {
      this.isScaleMinField = false;
    }
  }
}