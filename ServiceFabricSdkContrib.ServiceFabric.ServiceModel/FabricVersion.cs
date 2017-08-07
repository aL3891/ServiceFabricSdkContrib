// Decompiled with JetBrains decompiler
// Type: ServiceFabricSdkContrib.ServiceFabric.ServiceModel.FabricVersion
// Assembly: ServiceFabricSdkContrib.ServiceFabric.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\ServiceFabricSdkContrib.ServiceFabric.ServiceModel.dll

using System;
using System.Globalization;

namespace ServiceFabricSdkContrib.ServiceFabric.ServiceModel
{
  internal class FabricVersion
  {
    public static FabricVersion Invalid = new FabricVersion("0.0.0.0", string.Empty);

    public string CodeVersion { get; private set; }

    public string ConfigVersion { get; private set; }

    public FabricVersion(string codeVersion, string configVersion)
    {
      this.CodeVersion = codeVersion;
      this.ConfigVersion = configVersion;
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}:{1}", new object[2]
      {
        (object) this.CodeVersion,
        (object) this.ConfigVersion
      });
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
