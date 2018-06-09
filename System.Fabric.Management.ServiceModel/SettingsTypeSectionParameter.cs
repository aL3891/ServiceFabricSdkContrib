// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.SettingsTypeSectionParameter
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
  [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2011/01/fabric")]
  public class SettingsTypeSectionParameter
  {
    private string nameField;
    private string valueField;
    private bool mustOverrideField;
    private bool isEncryptedField;

    public SettingsTypeSectionParameter()
    {
      this.mustOverrideField = false;
      this.isEncryptedField = false;
    }

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
    public string Value
    {
      get
      {
        return this.valueField;
      }
      set
      {
        this.valueField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool MustOverride
    {
      get
      {
        return this.mustOverrideField;
      }
      set
      {
        this.mustOverrideField = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool IsEncrypted
    {
      get
      {
        return this.isEncryptedField;
      }
      set
      {
        this.isEncryptedField = value;
      }
    }
  }
}
