using System.Collections.Generic;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace AP.MobileToolkit.AAD
{
    public static class MsalExceptionExtensions
    {
        public static IDictionary<string, string> GetExceptionInfo(this MsalException msal, IDictionary<string, string> additionalValues = null)
        {
            var props = new Dictionary<string, string>
            {
                { "Data", JsonConvert.SerializeObject(msal.Data) },
                { "ErrorCode", msal.ErrorCode },
                { "HelpLink", msal.HelpLink },
                { "HResult", $"{msal.HResult}" },
                { "Source", msal.Source },
                { "TargetSite", msal.TargetSite.Name }
            };

            if (msal is MsalServiceException serviceException)
            {
                props.Add("Claims", serviceException.Claims);
                props.Add("ResponseBody", serviceException.ResponseBody);
                props.Add("StatusCode", $"{serviceException.StatusCode}");
            }

            if (additionalValues != null)
            {
                foreach (var pair in additionalValues)
                {
                    props.Add(pair.Key, pair.Value);
                }
            }

            return props;
        }
    }
}
