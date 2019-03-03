using System;

namespace AP.MobileToolkit.Device
{
    public interface IVibration
    {
        void Vibrate();
        void Vibrate(double duration);
        void Vibrate(TimeSpan duration);
        void Cancel();
    }
}