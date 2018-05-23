// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.FabricEndpointsType
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
  public class FabricEndpointsType
  {
    private InputEndpointType clientConnectionEndpointField;
    private InternalEndpointType leaseDriverEndpointField;
    private InternalEndpointType clusterConnectionEndpointField;
    private InputEndpointType httpGatewayEndpointField;
    private InputEndpointType httpApplicationGatewayEndpointField;
    private InternalEndpointType serviceConnectionEndpointField;
    private InternalEndpointType clusterManagerReplicatorEndpointField;
    private InternalEndpointType repairManagerReplicatorEndpointField;
    private InternalEndpointType namingReplicatorEndpointField;
    private InternalEndpointType failoverManagerReplicatorEndpointField;
    private InternalEndpointType imageStoreServiceReplicatorEndpointField;
    private InternalEndpointType upgradeServiceReplicatorEndpointField;
    private InternalEndpointType faultAnalysisServiceReplicatorEndpointField;
    private InternalEndpointType backupRestoreServiceReplicatorEndpointField;
    private InternalEndpointType upgradeOrchestrationServiceReplicatorEndpointField;
    private InternalEndpointType defaultReplicatorEndpointField;
    private FabricEndpointsTypeApplicationEndpoints applicationEndpointsField;
    private FabricEndpointsTypeEphemeralEndpoints ephemeralEndpointsField;

    public InputEndpointType ClientConnectionEndpoint
    {
      get
      {
        return this.clientConnectionEndpointField;
      }
      set
      {
        this.clientConnectionEndpointField = value;
      }
    }

    public InternalEndpointType LeaseDriverEndpoint
    {
      get
      {
        return this.leaseDriverEndpointField;
      }
      set
      {
        this.leaseDriverEndpointField = value;
      }
    }

    public InternalEndpointType ClusterConnectionEndpoint
    {
      get
      {
        return this.clusterConnectionEndpointField;
      }
      set
      {
        this.clusterConnectionEndpointField = value;
      }
    }

    public InputEndpointType HttpGatewayEndpoint
    {
      get
      {
        return this.httpGatewayEndpointField;
      }
      set
      {
        this.httpGatewayEndpointField = value;
      }
    }

    public InputEndpointType HttpApplicationGatewayEndpoint
    {
      get
      {
        return this.httpApplicationGatewayEndpointField;
      }
      set
      {
        this.httpApplicationGatewayEndpointField = value;
      }
    }

    public InternalEndpointType ServiceConnectionEndpoint
    {
      get
      {
        return this.serviceConnectionEndpointField;
      }
      set
      {
        this.serviceConnectionEndpointField = value;
      }
    }

    public InternalEndpointType ClusterManagerReplicatorEndpoint
    {
      get
      {
        return this.clusterManagerReplicatorEndpointField;
      }
      set
      {
        this.clusterManagerReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType RepairManagerReplicatorEndpoint
    {
      get
      {
        return this.repairManagerReplicatorEndpointField;
      }
      set
      {
        this.repairManagerReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType NamingReplicatorEndpoint
    {
      get
      {
        return this.namingReplicatorEndpointField;
      }
      set
      {
        this.namingReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType FailoverManagerReplicatorEndpoint
    {
      get
      {
        return this.failoverManagerReplicatorEndpointField;
      }
      set
      {
        this.failoverManagerReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType ImageStoreServiceReplicatorEndpoint
    {
      get
      {
        return this.imageStoreServiceReplicatorEndpointField;
      }
      set
      {
        this.imageStoreServiceReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType UpgradeServiceReplicatorEndpoint
    {
      get
      {
        return this.upgradeServiceReplicatorEndpointField;
      }
      set
      {
        this.upgradeServiceReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType FaultAnalysisServiceReplicatorEndpoint
    {
      get
      {
        return this.faultAnalysisServiceReplicatorEndpointField;
      }
      set
      {
        this.faultAnalysisServiceReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType BackupRestoreServiceReplicatorEndpoint
    {
      get
      {
        return this.backupRestoreServiceReplicatorEndpointField;
      }
      set
      {
        this.backupRestoreServiceReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType UpgradeOrchestrationServiceReplicatorEndpoint
    {
      get
      {
        return this.upgradeOrchestrationServiceReplicatorEndpointField;
      }
      set
      {
        this.upgradeOrchestrationServiceReplicatorEndpointField = value;
      }
    }

    public InternalEndpointType DefaultReplicatorEndpoint
    {
      get
      {
        return this.defaultReplicatorEndpointField;
      }
      set
      {
        this.defaultReplicatorEndpointField = value;
      }
    }

    public FabricEndpointsTypeApplicationEndpoints ApplicationEndpoints
    {
      get
      {
        return this.applicationEndpointsField;
      }
      set
      {
        this.applicationEndpointsField = value;
      }
    }

    public FabricEndpointsTypeEphemeralEndpoints EphemeralEndpoints
    {
      get
      {
        return this.ephemeralEndpointsField;
      }
      set
      {
        this.ephemeralEndpointsField = value;
      }
    }
  }
}
