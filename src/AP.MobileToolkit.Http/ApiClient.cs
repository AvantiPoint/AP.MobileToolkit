using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Prism.Logging;
using Xamarin.Essentials.Interfaces;

namespace AP.MobileToolkit.Http
{
    public delegate Task<bool> SaveApiObject<T>(T obj);

    public class ApiClient : ApiClientBase
    {
        protected IAppInfo AppInfo { get; }

        protected IDeviceInfo DeviceInfo { get; }

        public ApiClient(IApiClientOptions options, IAuthenticationHandler authenticationHandler, ILogger logger, IAppInfo appInfo, IDeviceInfo deviceInfo)
            : base(options, authenticationHandler, logger)
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
            var appName = Regex.Replace(AppInfo.Name, @"\s", string.Empty);
            var agentHeader = ProductHeaderValue.Parse($"{appName}/{AppInfo.VersionString}");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(agentHeader));
            client.DefaultRequestHeaders.Add("X-MobileAppVer", AppInfo.VersionString);
            client.DefaultRequestHeaders.Add("X-DeviceModel", DeviceInfo.Model);
            client.DefaultRequestHeaders.Add("X-DeviceManufacturer", DeviceInfo.Manufacturer);
            client.DefaultRequestHeaders.Add("X-DeviceName", DeviceInfo.Name);
            client.DefaultRequestHeaders.Add("X-DevicePlatform", $"{DeviceInfo.Platform}");
            client.DefaultRequestHeaders.Add("X-DeviceIdiom", $"{DeviceInfo.Idiom}");

            if (!string.IsNullOrWhiteSpace(InstallId))
            {
                client.DefaultRequestHeaders.Add("X-ClientId", InstallId);
            }
        }
    }
}
