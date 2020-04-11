using System;
using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    internal class MsalResult : IMsalResult
    {
        public MsalResult(AuthenticationResult result)
        {
            Result = result;
        }

        public MsalResult(Exception ex)
        {
            Exception = ex;
        }

        public bool Success => Exception is null && !string.IsNullOrEmpty(Result?.AccessToken);

        public AuthenticationResult Result { get; }

        public Exception Exception { get; }

        public bool UserCancelled => Exception != null && Exception is MsalException msalException && msalException.ErrorCode == MsalError.AuthenticationCanceledError;
    }
}
