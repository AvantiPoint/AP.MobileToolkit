using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IBrowser
    {
        Task OpenAsync(string uri);
        Task OpenAsync(string uri, BrowserLaunchMode launchType);
        Task OpenAsync(Uri uri);
        Task OpenAsync(Uri uri, BrowserLaunchMode launchType);
    }
}
