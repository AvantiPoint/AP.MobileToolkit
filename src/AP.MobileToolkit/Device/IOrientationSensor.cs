using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IOrientationSensor
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
}
