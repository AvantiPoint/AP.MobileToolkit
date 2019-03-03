using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IAccelerometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
}
