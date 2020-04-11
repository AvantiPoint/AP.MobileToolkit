using System;
using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public interface IMsalResult
    {
        bool Success { get; }
        AuthenticationResult Result { get; }
        Exception Exception { get; }
        bool UserCancelled { get; }
    }
}
