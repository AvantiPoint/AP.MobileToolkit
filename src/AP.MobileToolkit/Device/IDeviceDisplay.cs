using System;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IDeviceDisplay
    {
        bool KeepScreenOn { get; set; }
        DisplayInfo MainDisplayInfo { get; }
        event EventHandler<DisplayInfoChangedEventArgs> MainDisplayInfoChanged;
    }
}
