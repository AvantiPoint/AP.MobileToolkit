using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AP.CrossPlatform.Extensions;
using Xamarin.Essentials.Interfaces;

namespace AP.MobileToolkit.Http
{
    public class ClientInfoHeaderHandler : DelegatingHandler
    {
        public ClientInfoHeaderHandler(IAppInfo appInfo, IDeviceInfo deviceInfo)
            : this(appInfo, deviceInfo, new HttpClientHandler())
        {
        }

        public ClientInfoHeaderHandler(IAppInfo appInfo, IDeviceInfo deviceInfo, string installId)
            : this(appInfo, deviceInfo, installId, new HttpClientHandler())
        {
        }

        public ClientInfoHeaderHandler(IAppInfo appInfo, IDeviceInfo deviceInfo, HttpMessageHandler innerHandler)
            : this(appInfo, deviceInfo, string.Empty, innerHandler)
        {
        }

        public ClientInfoHeaderHandler(IAppInfo appInfo, IDeviceInfo deviceInfo, string installId, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            AppInfo = appInfo;
            DeviceInfo = deviceInfo;
            InstallId = installId ?? string.Empty;
        }

        protected IAppInfo AppInfo { get; }

        protected IDeviceInfo DeviceInfo { get; }

        protected string InstallId { get; }

        protected virtual string InstallIdHeader => "X-ClientId";

        /// <summary>
        /// Gets the install id. Typically the device token used for Push Notifications
        /// </summary>
        /// <returns>The Device Token</returns>
        protected virtual string GetInstallId() => InstallId;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            var appName = Regex.Replace(AppInfo.Name, @"\s", string.Empty).ToASCII();
            var agentHeader = ProductHeaderValue.Parse($"{appName}/{AppInfo.VersionString.ToASCII()}");
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(agentHeader));
            request.Headers.Add("X-MobileAppVer", AppInfo.VersionString.ToASCII());
            request.Headers.Add("X-DeviceModel", DeviceInfo.Model.ToASCII());
            request.Headers.Add("X-DeviceManufacturer", DeviceInfo.Manufacturer.ToASCII());
            request.Headers.Add("X-DeviceName", DeviceInfo.Name.ToASCII());
            request.Headers.Add("X-DevicePlatform", $"{DeviceInfo.Platform}");
            request.Headers.Add("X-DeviceIdiom", $"{DeviceInfo.Idiom}");

            var installId = GetInstallId();
            if (!string.IsNullOrWhiteSpace(installId))
            {
                request.Headers.Add(InstallIdHeader, installId);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
