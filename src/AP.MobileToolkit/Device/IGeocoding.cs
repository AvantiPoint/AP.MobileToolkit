using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IGeocoding
    {
        Task<IEnumerable<Placemark>> GetPlacemarksAsync(Location location);
        Task<IEnumerable<Placemark>> GetPlacemarksAsync(double latitude, double longitude);
        Task<IEnumerable<Location>> GetLocationsAsync(string address);
    }
}
