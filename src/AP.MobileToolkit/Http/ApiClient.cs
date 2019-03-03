using System.Net.Http.Headers;
using System.Threading.Tasks;
using AP.MobileToolkit.Device;
using Prism.Logging;

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
            SetDefaultHeaders();
        }

        protected virtual void SetDefaultHeaders()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            var agentHeader = ProductHeaderValue.Parse($"{AppInfo.Name}/{AppInfo.VersionString}");
            Client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(agentHeader));
            Client.DefaultRequestHeaders.Add("X-MobileAppVer", AppInfo.VersionString);
            Client.DefaultRequestHeaders.Add("X-DeviceModel", DeviceInfo.Model);
            Client.DefaultRequestHeaders.Add("X-DeviceManufacturer", DeviceInfo.Manufacturer);
            Client.DefaultRequestHeaders.Add("X-DeviceName", DeviceInfo.Name);
            Client.DefaultRequestHeaders.Add("X-DevicePlatform", $"{DeviceInfo.Platform}");
            Client.DefaultRequestHeaders.Add("X-DeviceIdiom", $"{DeviceInfo.Idiom}");

            if(!string.IsNullOrWhiteSpace(InstallId))
            {
                Client.DefaultRequestHeaders.Add("X-ClientId", InstallId);
            }
        }
    }
}
