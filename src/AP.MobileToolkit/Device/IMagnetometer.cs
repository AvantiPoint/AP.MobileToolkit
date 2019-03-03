using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IMagnetometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
}
