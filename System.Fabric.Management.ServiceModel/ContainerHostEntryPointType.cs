// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.ContainerHostEntryPointType
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
  public class ContainerHostEntryPointType
  {
    private string imageNameField;
    private string commandsField;
    private string fromSourceField;

    public string ImageName
    {
      get
      {
        return this.imageNameField;
      }
      set
      {
        this.imageNameField = value;
      }
    }

    public string Commands
    {
      get
      {
        return this.commandsField;
      }
      set
      {
        this.commandsField = value;
      }
    }

    public string FromSource
    {
      get
      {
        return this.fromSourceField;
      }
      set
      {
        this.fromSourceField = value;
      }
    }
  }
}
