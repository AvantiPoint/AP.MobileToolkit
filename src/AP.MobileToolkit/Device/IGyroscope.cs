using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IGyroscope
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
}
