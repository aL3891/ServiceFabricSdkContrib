// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.RolloutVersion
// Assembly: System.Fabric.Management.ServiceModel, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C6D32D4D-966E-4EA3-BD3A-F4CF14D36DBC
// Assembly location: C:\Git\ServiceFabricSdkContrib\ServiceFabricSdkContrib.MsBuild\bin\Debug\netstandard2.0\publish\runtimes\win\lib\netstandard2.0\System.Fabric.Management.ServiceModel.dll

using System.Globalization;

namespace System.Fabric.Management.ServiceModel
{
  internal class RolloutVersion
  {
    private RolloutVersion(long majorVersion, long minorVersion)
    {
      if (majorVersion < 1L)
        throw new ArgumentException("Value has to be a positive integer.", nameof (majorVersion));
      if (minorVersion < 0L)
        throw new ArgumentException("Value has to be a non-negative integer.", nameof (minorVersion));
      this.MajorVersion = majorVersion;
      this.MinorVersion = minorVersion;
    }

    public long MajorVersion { get; private set; }

    public long MinorVersion { get; private set; }

    public static RolloutVersion CreateRolloutVersion(long majorVersion = 1)
    {
      return new RolloutVersion(majorVersion, 0L);
    }

    public static RolloutVersion CreateRolloutVersion(string rolloutVersion)
    {
      if (rolloutVersion == null)
        throw new ArgumentNullException(nameof (rolloutVersion));
      string[] strArray = rolloutVersion.Split('.');
      long result1;
      long result2;
      if (strArray.Length == 2 && long.TryParse(strArray[0], out result1) && long.TryParse(strArray[1], out result2))
        return new RolloutVersion(result1, result2);
      throw new ArgumentException("RolloutVersion string {0} is not in the correct format.", rolloutVersion);
    }

    public RolloutVersion NextMajorRolloutVersion()
    {
      return new RolloutVersion(this.MajorVersion + 1L, 0L);
    }

    public RolloutVersion NextMinorRolloutVersion()
    {
      return new RolloutVersion(this.MajorVersion, this.MinorVersion + 1L);
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}.{1}", (object) this.MajorVersion, (object) this.MinorVersion);
    }

    public override bool Equals(object obj)
    {
      RolloutVersion rolloutVersion = obj as RolloutVersion;
      if (rolloutVersion != null && rolloutVersion.MajorVersion == this.MajorVersion)
        return rolloutVersion.MinorVersion == this.MinorVersion;
      return false;
    }

    public override int GetHashCode()
    {
      return (int) (this.MajorVersion * 1000L + this.MinorVersion);
    }
  }
}
