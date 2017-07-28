﻿// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.CertificatesType
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
  [XmlType(Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class CertificatesType
  {
    private FabricCertificateType clusterCertificateField;
    private FabricCertificateType serverCertificateField;
    private FabricCertificateType clientCertificateField;
    private FabricCertificateType userRoleClientCertificateField;

    public FabricCertificateType ClusterCertificate
    {
      get
      {
        return this.clusterCertificateField;
      }
      set
      {
        this.clusterCertificateField = value;
      }
    }

    public FabricCertificateType ServerCertificate
    {
      get
      {
        return this.serverCertificateField;
      }
      set
      {
        this.serverCertificateField = value;
      }
    }

    public FabricCertificateType ClientCertificate
    {
      get
      {
        return this.clientCertificateField;
      }
      set
      {
        this.clientCertificateField = value;
      }
    }

    public FabricCertificateType UserRoleClientCertificate
    {
      get
      {
        return this.userRoleClientCertificateField;
      }
      set
      {
        this.userRoleClientCertificateField = value;
      }
    }
  }
}