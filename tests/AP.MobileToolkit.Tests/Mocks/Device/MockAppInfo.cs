using System;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class MockAppInfo : IAppInfo
    {
        public string PackageName => "com.avantipoint.mobiletoolkit";

        public string Name => "MobileToolkit";

        public string VersionString => Version.ToString();

        public Version Version => typeof(IAppInfo).Assembly.GetName().Version;

        public string BuildString => Version.Build.ToString();

        public AppTheme RequestedTheme { get; set; } = AppTheme.Light;

        public void ShowSettingsUI()
        {
        }
    }
}
