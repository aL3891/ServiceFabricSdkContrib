// Decompiled with JetBrains decompiler
// Type: System.Fabric.Management.ServiceModel.RolloutVersion
// Assembly: System.Fabric.Management.ServiceModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9A8DFECE-1E98-4DC4-86BF-EB9CA495B5B8
// Assembly location: D:\Documents\GitHub\ServiceFabricSdkContrib\TestSolution\packages\Microsoft.ServiceFabric.5.6.220\lib\net45\System.Fabric.Management.ServiceModel.dll

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Fabric.Management.ServiceModel
{
  internal class RolloutVersion
  {
    public long MajorVersion { get; private set; }

    public long MinorVersion { get; private set; }

    private RolloutVersion(long majorVersion, long minorVersion)
    {
      if (majorVersion < 1L)
        throw new ArgumentException("Value has to be a positive integer.", "majorVersion");
      if (minorVersion < 0L)
        throw new ArgumentException("Value has to be a non-negative integer.", "minorVersion");
      this.MajorVersion = majorVersion;
      this.MinorVersion = minorVersion;
    }

    public static RolloutVersion CreateRolloutVersion(long majorVersion = 1)
    {
      return new RolloutVersion(majorVersion, 0L);
    }

    public static RolloutVersion CreateRolloutVersion(string rolloutVersion)
    {
      if (rolloutVersion == null)
        throw new ArgumentNullException("rolloutVersion");
      string[] strArray = rolloutVersion.Split('.');
      long result1;
      long result2;
      if (strArray.Length == 2 && long.TryParse(strArray[0], out result1) && long.TryParse(strArray[1], out result2))
        return new RolloutVersion(result1, result2);
      throw new ArgumentException("RolloutVersion string {0} is not in the correct format.", rolloutVersion);
    }

    [SuppressMessage("Microsoft.Performance", "CA1811", Justification = "Used in some dlls while not in others")]
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
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}.{1}", new object[2]
      {
        (object) this.MajorVersion,
        (object) this.MinorVersion
      });
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
