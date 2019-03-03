using System;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IDeviceInfo
    {
        string Model { get; }
        string Manufacturer { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        DevicePlatform Platform { get; }
        DeviceIdiom Idiom { get; }
        DeviceType DeviceType { get; }
    }
}
