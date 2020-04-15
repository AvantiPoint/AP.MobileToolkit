using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AP.CrossPlatform.Extensions;
using Prism.Logging;
using Xamarin.Essentials.Interfaces;

namespace AP.MobileToolkit.Http
{
    public delegate Task<bool> SaveApiObject<T>(T obj);

    public class ApiClient : ApiClientBase
    {
        protected IAppInfo AppInfo { get; }

        protected IDeviceInfo DeviceInfo { get; }

        public ApiClient(IApiClientOptions options, ILogger logger, IAppInfo appInfo, IDeviceInfo deviceInfo)
            : base(options, logger)
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

            if (!string.IsNullOrWhiteSpace(InstallId))
            {
                client.DefaultRequestHeaders.Add("X-ClientId", InstallId);
            }
        }

        protected override Task<string> GetTokenAsync()
        {
            Logger.Warn("You must override ApiClient.GetTokenAsync() to make authenticated calls.");
            throw new NotImplementedException();
        }
    }
}
