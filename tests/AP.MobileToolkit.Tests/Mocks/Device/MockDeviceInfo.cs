using System;
using AP.MobileToolkit.Device;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class MockDeviceInfo : IDeviceInfo
    {
        public string Model => "Automated";
        public string Manufacturer => "AvantiPoint";
        public string Name => Environment.MachineName;
        public string VersionString => Version.ToString();
        public Version Version => typeof(IDeviceInfo).Assembly.GetName().Version;
        public DevicePlatform Platform => DevicePlatform.Unknown;
        public DeviceIdiom Idiom => DeviceIdiom.Unknown;
        public DeviceType DeviceType => DeviceType.Virtual;
    }
}
