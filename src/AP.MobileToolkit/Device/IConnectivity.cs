using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IConnectivity
    {
        NetworkAccess NetworkAccess { get; }
        IEnumerable<ConnectionProfile> ConnectionProfiles { get; }
        event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;
    }
}
