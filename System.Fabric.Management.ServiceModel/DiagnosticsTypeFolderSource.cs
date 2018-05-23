// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.DiagnosticsTypeFolderSource
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
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class DiagnosticsTypeFolderSource
  {
    private DiagnosticsTypeFolderSourceDestinations destinationsField;
    private ParameterType[] parametersField;
    private string isEnabledField;
    private string relativeFolderPathField;
    private string dataDeletionAgeInDaysField;

    public DiagnosticsTypeFolderSourceDestinations Destinations
    {
      get
      {
        return this.destinationsField;
      }
      set
      {
        this.destinationsField = value;
      }
    }

    [XmlArrayItem("Parameter", IsNullable = false)]
    public ParameterType[] Parameters
    {
      get
      {
        return this.parametersField;
      }
      set
      {
        this.parametersField = value;
      }
    }

    [XmlAttribute]
    public string IsEnabled
    {
      get
      {
        return this.isEnabledField;
      }
      set
      {
        this.isEnabledField = value;
      }
    }

    [XmlAttribute]
    public string RelativeFolderPath
    {
      get
      {
        return this.relativeFolderPathField;
      }
      set
      {
        this.relativeFolderPathField = value;
      }
    }

    [XmlAttribute]
    public string DataDeletionAgeInDays
    {
      get
      {
        return this.dataDeletionAgeInDaysField;
      }
      set
      {
        this.dataDeletionAgeInDaysField = value;
      }
    }
  }
}
