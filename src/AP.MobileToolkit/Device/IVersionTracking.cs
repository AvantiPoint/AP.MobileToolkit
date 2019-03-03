using System.Collections.Generic;

namespace AP.MobileToolkit.Device
{
    public interface IVersionTracking
    {
        void Track();
        bool IsFirstLaunchForVersion(string version);
        bool IsFirstLaunchForBuild(string build);
        bool IsFirstLaunchEver { get; }
        bool IsFirstLaunchForCurrentVersion { get; }
        bool IsFirstLaunchForCurrentBuild { get; }
        string CurrentVersion { get; }
        string CurrentBuild { get; }
        string PreviousVersion { get; }
        string PreviousBuild { get; }
        string FirstInstalledVersion { get; }
        string FirstInstalledBuild { get; }
        IEnumerable<string> VersionHistory { get; }
        IEnumerable<string> BuildHistory { get; }
    }
}
