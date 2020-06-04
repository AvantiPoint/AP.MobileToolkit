using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using AP.CrossPlatform.Extensions;
using Prism.Logging;
using Xamarin.Essentials.Interfaces;

namespace AP.MobileToolkit.Http
{
    public abstract class ApiClient : ApiClientBase
    {
        protected IAppInfo AppInfo { get; }

        protected IDeviceInfo DeviceInfo { get; }

        protected ApiClient(ILogger logger, IAppInfo appInfo, IDeviceInfo deviceInfo)
            : base(logger)
        {
            AppInfo = appInfo;
            DeviceInfo = deviceInfo;
        }

        protected override void SetDefaultHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            var appName = Regex.Replace(AppInfo.Name, @"\s", string.Empty).ToASCII();
            var agentHeader = ProductHeaderValue.Parse($"{appName}/{AppInfo.VersionString.ToASCII()}");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(agentHeader));
            client.DefaultRequestHeaders.Add("X-MobileAppVer", AppInfo.VersionString.ToASCII());
            client.DefaultRequestHeaders.Add("X-DeviceModel", DeviceInfo.Model.ToASCII());
            client.DefaultRequestHeaders.Add("X-DeviceManufacturer", DeviceInfo.Manufacturer.ToASCII());
            client.DefaultRequestHeaders.Add("X-DeviceName", DeviceInfo.Name.ToASCII());
            client.DefaultRequestHeaders.Add("X-DevicePlatform", $"{DeviceInfo.Platform}");
            client.DefaultRequestHeaders.Add("X-DeviceIdiom", $"{DeviceInfo.Idiom}");

            if (TryGetInstallId(out var installId))
            {
                client.DefaultRequestHeaders.Add("X-ClientId", installId);
            }
        }
    }
}
