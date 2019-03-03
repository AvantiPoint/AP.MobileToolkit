using System;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IBattery
    {
        double ChargeLevel { get; }
        EnergySaverStatus EnergySaverStatus { get; }
        BatteryState State { get; }
        BatteryPowerSource PowerSource { get; }
        event EventHandler<BatteryInfoChangedEventArgs> BatteryInfoChanged;
        event EventHandler<EnergySaverStatusChangedEventArgs> EnergySaverStatusChanged;
    }
}
