using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IGeolocation
    {
        Task<Location> GetLastKnownLocationAsync();
        Task<Location> GetLocationAsync();
        Task<Location> GetLocationAsync(GeolocationRequest request);
        Task<Location> GetLocationAsync(GeolocationRequest request, CancellationToken cancelToken);
    }
}
