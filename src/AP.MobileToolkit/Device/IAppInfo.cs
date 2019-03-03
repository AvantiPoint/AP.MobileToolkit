using System;

namespace AP.MobileToolkit.Device
{
    public interface IAppInfo
    {
        void ShowSettingsUI();
        string PackageName { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        string BuildString { get; }
    }
}
