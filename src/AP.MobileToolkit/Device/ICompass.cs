using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface ICompass
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
}
