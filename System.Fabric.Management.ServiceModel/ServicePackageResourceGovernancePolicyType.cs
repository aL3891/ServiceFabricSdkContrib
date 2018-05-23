// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ServicePackageResourceGovernancePolicyType
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
  public class ServicePackageResourceGovernancePolicyType
  {
    private string cpuCoresField;
    private string memoryInMBField;

    public ServicePackageResourceGovernancePolicyType()
    {
      this.cpuCoresField = "0";
      this.memoryInMBField = "0";
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string CpuCores
    {
      get
      {
        return this.cpuCoresField;
      }
      set
      {
        this.cpuCoresField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("0")]
    public string MemoryInMB
    {
      get
      {
        return this.memoryInMBField;
      }
      set
      {
        this.memoryInMBField = value;
      }
    }
  }
}
