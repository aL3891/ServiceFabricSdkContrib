// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.PaaSRoleType
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
  public class PaaSRoleType
  {
    private string roleNameField;
    private string nodeTypeRefField;
    private int roleNodeCountField;

    [XmlAttribute]
    public string RoleName
    {
      get
      {
        return this.roleNameField;
      }
      set
      {
        this.roleNameField = value;
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

    [XmlAttribute]
    public int RoleNodeCount
    {
      get
      {
        return this.roleNodeCountField;
      }
      set
      {
        this.roleNodeCountField = value;
      }
    }
  }
}
