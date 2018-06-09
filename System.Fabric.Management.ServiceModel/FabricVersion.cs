// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.FabricVersion
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.Globalization;

namespace System.Fabric.Management.ServiceModel
{
  internal class FabricVersion
  {
    public static FabricVersion Invalid = new FabricVersion("0.0.0.0", string.Empty);

    public FabricVersion(string codeVersion, string configVersion)
    {
      this.CodeVersion = codeVersion;
      this.ConfigVersion = configVersion;
    }

    public string CodeVersion { get; private set; }

    public string ConfigVersion { get; private set; }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}:{1}", (object) this.CodeVersion, (object) this.ConfigVersion);
    }

    public static bool TryParse(string fabricVersionString, out FabricVersion fabricVersion)
    {
      fabricVersion = (FabricVersion) null;
      if (string.IsNullOrEmpty(fabricVersionString))
        return false;
      int length = fabricVersionString.IndexOf(':');
      if (length == -1)
        return false;
      string codeVersion = fabricVersionString.Substring(0, length);
      string configVersion = fabricVersionString.Substring(length + 1);
      fabricVersion = new FabricVersion(codeVersion, configVersion);
      return true;
    }

    public override bool Equals(object obj)
    {
      FabricVersion fabricVersion = obj as FabricVersion;
      if (fabricVersion != null && string.Equals(this.CodeVersion, fabricVersion.CodeVersion, StringComparison.OrdinalIgnoreCase))
        return string.Equals(this.ConfigVersion, this.ConfigVersion, StringComparison.OrdinalIgnoreCase);
      return false;
    }

    public override int GetHashCode()
    {
      return this.CodeVersion.GetHashCode() + this.ConfigVersion.GetHashCode();
    }
  }
}
