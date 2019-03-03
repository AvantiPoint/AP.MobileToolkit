using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Runtime.Serialization;

namespace AP.MobileToolkit.Device
{
#pragma warning disable CS1066
    public class EssentialsImplementation : IAccelerometer, IAppInfo, IBattery, IBrowser, IClipboard, ICompass, 
        IConnectivity, IDeviceDisplay, IDeviceInfo, IEmail, IFileSystem, IFlashlight, IGeocoding, 
        IGeolocation, IGyroscope, IMagnetometer, IMainThread, IOrientationSensor, IPhoneDialer, IPreferences, 
        ISecureStorage, ISms, ITextToSpeech, IVersionTracking, IVibration
    {
        public static readonly IEnumerable<Type> ServiceTypes = new Type[]{
            typeof(IAccelerometer),
            typeof(IAppInfo),
            typeof(IBattery),
            typeof(IBrowser),
            typeof(IClipboard),
            typeof(ICompass),
            typeof(IConnectivity),
            typeof(IDeviceDisplay),
            typeof(IDeviceInfo),
            typeof(IEmail),
            typeof(IFileSystem),
            typeof(IFlashlight),
            typeof(IGeocoding),
            typeof(IGeolocation),
            typeof(IGyroscope),
            typeof(IMagnetometer),
            typeof(IMainThread),
            typeof(IOrientationSensor),
            typeof(IPhoneDialer),
            typeof(IPreferences),
            typeof(ISecureStorage),
            typeof(ISms),
            typeof(ITextToSpeech),
            typeof(IVersionTracking),
            typeof(IVibration)
        };

        void IAccelerometer.Start(SensorSpeed sensorSpeed)
                           => Xamarin.Essentials.Accelerometer.Start(sensorSpeed);

        void IAccelerometer.Stop()
                           => Xamarin.Essentials.Accelerometer.Stop();

        bool IAccelerometer.IsMonitoring
                           => Xamarin.Essentials.Accelerometer.IsMonitoring;

        void IAppInfo.ShowSettingsUI()
                     => Xamarin.Essentials.AppInfo.ShowSettingsUI();

        string IAppInfo.PackageName
                       => Xamarin.Essentials.AppInfo.PackageName;

        string IAppInfo.Name
                       => Xamarin.Essentials.AppInfo.Name;

        string IAppInfo.VersionString
                       => Xamarin.Essentials.AppInfo.VersionString;

        Version IAppInfo.Version
                        => Xamarin.Essentials.AppInfo.Version;

        string IAppInfo.BuildString
                       => Xamarin.Essentials.AppInfo.BuildString;

        double IBattery.ChargeLevel
                       => Xamarin.Essentials.Battery.ChargeLevel;

        EnergySaverStatus IBattery.EnergySaverStatus
                        => Xamarin.Essentials.Battery.EnergySaverStatus;

        BatteryState IBattery.State
                             => Xamarin.Essentials.Battery.State;

        BatteryPowerSource IBattery.PowerSource
                                   => Xamarin.Essentials.Battery.PowerSource;

        event EventHandler<BatteryInfoChangedEventArgs> IBattery.BatteryInfoChanged
        {
            add => Xamarin.Essentials.Battery.BatteryInfoChanged += value;
            remove => Xamarin.Essentials.Battery.BatteryInfoChanged -= value;
        }

        event EventHandler<EnergySaverStatusChangedEventArgs> IBattery.EnergySaverStatusChanged
        {
            add => Xamarin.Essentials.Battery.EnergySaverStatusChanged += value;
            remove => Xamarin.Essentials.Battery.EnergySaverStatusChanged -= value;
        }

        Task IBrowser.OpenAsync(string uri)
                     => Xamarin.Essentials.Browser.OpenAsync(uri);

        Task IBrowser.OpenAsync(string uri, BrowserLaunchMode launchType)
                     => Xamarin.Essentials.Browser.OpenAsync(uri, launchType);

        Task IBrowser.OpenAsync(Uri uri)
                     => Xamarin.Essentials.Browser.OpenAsync(uri);

        Task IBrowser.OpenAsync(Uri uri, BrowserLaunchMode launchType)
                     => Xamarin.Essentials.Browser.OpenAsync(uri, launchType);

        Task IClipboard.SetTextAsync(string text)
                       => Xamarin.Essentials.Clipboard.SetTextAsync(text);

        Task<string> IClipboard.GetTextAsync()
                               => Xamarin.Essentials.Clipboard.GetTextAsync();

        bool IClipboard.HasText
                       => Xamarin.Essentials.Clipboard.HasText;

        void ICompass.Start(SensorSpeed sensorSpeed)
                     => Xamarin.Essentials.Compass.Start(sensorSpeed);

        void ICompass.Stop()
                     => Xamarin.Essentials.Compass.Stop();

        bool ICompass.IsMonitoring
                     => Xamarin.Essentials.Compass.IsMonitoring;

        NetworkAccess IConnectivity.NetworkAccess
                                   => Xamarin.Essentials.Connectivity.NetworkAccess;

        IEnumerable<ConnectionProfile> IConnectivity.ConnectionProfiles
                                                    => Xamarin.Essentials.Connectivity.ConnectionProfiles;

        event EventHandler<ConnectivityChangedEventArgs> IConnectivity.ConnectivityChanged
        {
            add => Xamarin.Essentials.Connectivity.ConnectivityChanged += value;
            remove => Xamarin.Essentials.Connectivity.ConnectivityChanged -= value;
        }

        bool IDeviceDisplay.KeepScreenOn
        {
            get => Xamarin.Essentials.DeviceDisplay.KeepScreenOn;
            set => Xamarin.Essentials.DeviceDisplay.KeepScreenOn = value;
        }

        DisplayInfo IDeviceDisplay.MainDisplayInfo => Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;

        event EventHandler<DisplayInfoChangedEventArgs> IDeviceDisplay.MainDisplayInfoChanged
        {
            add => Xamarin.Essentials.DeviceDisplay.MainDisplayInfoChanged += value;
            remove => Xamarin.Essentials.DeviceDisplay.MainDisplayInfoChanged -= value;
        }

        string IDeviceInfo.Model
                          => Xamarin.Essentials.DeviceInfo.Model;

        string IDeviceInfo.Manufacturer
                          => Xamarin.Essentials.DeviceInfo.Manufacturer;

        string IDeviceInfo.Name
                          => Xamarin.Essentials.DeviceInfo.Name;

        string IDeviceInfo.VersionString
                          => Xamarin.Essentials.DeviceInfo.VersionString;

        Version IDeviceInfo.Version
                           => Xamarin.Essentials.DeviceInfo.Version;

        DevicePlatform IDeviceInfo.Platform
                          => Xamarin.Essentials.DeviceInfo.Platform;

        DeviceIdiom IDeviceInfo.Idiom
                          => Xamarin.Essentials.DeviceInfo.Idiom;

        DeviceType IDeviceInfo.DeviceType
                              => Xamarin.Essentials.DeviceInfo.DeviceType;

        Task IEmail.ComposeAsync()
                   => Xamarin.Essentials.Email.ComposeAsync();

        Task IEmail.ComposeAsync(string subject, string body, params string[] to)
                   => Xamarin.Essentials.Email.ComposeAsync(subject, body, to);

        Task IEmail.ComposeAsync(EmailMessage message)
                   => Xamarin.Essentials.Email.ComposeAsync(message);

        Task<Stream> IFileSystem.OpenAppPackageFileAsync(string filename)
                                => Xamarin.Essentials.FileSystem.OpenAppPackageFileAsync(filename);

        string IFileSystem.CacheDirectory
                          => Xamarin.Essentials.FileSystem.CacheDirectory;

        string IFileSystem.AppDataDirectory
                          => Xamarin.Essentials.FileSystem.AppDataDirectory;

        Task IFlashlight.TurnOnAsync()
                        => Xamarin.Essentials.Flashlight.TurnOnAsync();

        Task IFlashlight.TurnOffAsync()
                        => Xamarin.Essentials.Flashlight.TurnOffAsync();

        Task<IEnumerable<Placemark>> IGeocoding.GetPlacemarksAsync(Location location)
                                               => Xamarin.Essentials.Geocoding.GetPlacemarksAsync(location);

        Task<IEnumerable<Placemark>> IGeocoding.GetPlacemarksAsync(double latitude, double longitude)
                                               => Xamarin.Essentials.Geocoding.GetPlacemarksAsync(latitude, longitude);

        Task<IEnumerable<Location>> IGeocoding.GetLocationsAsync(string address)
                                              => Xamarin.Essentials.Geocoding.GetLocationsAsync(address);

        Task<Location> IGeolocation.GetLastKnownLocationAsync()
                                   => Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();

        Task<Location> IGeolocation.GetLocationAsync()
                                   => Xamarin.Essentials.Geolocation.GetLocationAsync();

        Task<Location> IGeolocation.GetLocationAsync(GeolocationRequest request)
                                   => Xamarin.Essentials.Geolocation.GetLocationAsync(request);

        Task<Location> IGeolocation.GetLocationAsync(GeolocationRequest request, CancellationToken cancelToken)
                                   => Xamarin.Essentials.Geolocation.GetLocationAsync(request, cancelToken);

        void IGyroscope.Start(SensorSpeed sensorSpeed)
                       => Xamarin.Essentials.Gyroscope.Start(sensorSpeed);

        void IGyroscope.Stop()
                       => Xamarin.Essentials.Gyroscope.Stop();

        bool IGyroscope.IsMonitoring
                       => Xamarin.Essentials.Gyroscope.IsMonitoring;

        void IMagnetometer.Start(SensorSpeed sensorSpeed)
                          => Xamarin.Essentials.Magnetometer.Start(sensorSpeed);

        void IMagnetometer.Stop()
                          => Xamarin.Essentials.Magnetometer.Stop();

        bool IMagnetometer.IsMonitoring
                          => Xamarin.Essentials.Magnetometer.IsMonitoring;

        void IMainThread.BeginInvokeOnMainThread(Action action)
                        => Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(action);

        bool IMainThread.IsMainThread
                        => Xamarin.Essentials.MainThread.IsMainThread;

        void IOrientationSensor.Start(SensorSpeed sensorSpeed)
                               => Xamarin.Essentials.OrientationSensor.Start(sensorSpeed);

        void IOrientationSensor.Stop()
                               => Xamarin.Essentials.OrientationSensor.Stop();

        bool IOrientationSensor.IsMonitoring
                               => Xamarin.Essentials.OrientationSensor.IsMonitoring;

        void IPhoneDialer.Open(string number)
                         => Xamarin.Essentials.PhoneDialer.Open(number);

        bool IPreferences.ContainsKey(string key)
                         => Xamarin.Essentials.Preferences.ContainsKey(key);

        void IPreferences.Remove(string key)
                         => Xamarin.Essentials.Preferences.Remove(key);

        void IPreferences.Clear()
                         => Xamarin.Essentials.Preferences.Clear();

        string IPreferences.Get(string key, string defaultValue)
                           => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        bool IPreferences.Get(string key, bool defaultValue)
                         => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        int IPreferences.Get(string key, int defaultValue)
                        => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        double IPreferences.Get(string key, double defaultValue)
                           => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        float IPreferences.Get(string key, float defaultValue)
                          => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        long IPreferences.Get(string key, long defaultValue)
                         => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        void IPreferences.Set(string key, string value)
                         => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, bool value)
                         => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, int value)
                         => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, double value)
                         => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, float value)
                         => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, long value)
                         => Xamarin.Essentials.Preferences.Set(key, value);

        bool IPreferences.ContainsKey(string key, string sharedName)
                         => Xamarin.Essentials.Preferences.ContainsKey(key, sharedName);

        void IPreferences.Remove(string key, string sharedName)
                         => Xamarin.Essentials.Preferences.Remove(key, sharedName);

        void IPreferences.Clear(string sharedName)
                         => Xamarin.Essentials.Preferences.Clear(sharedName);

        string IPreferences.Get(string key, string defaultValue, string sharedName)
                           => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        bool IPreferences.Get(string key, bool defaultValue, string sharedName)
                         => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        int IPreferences.Get(string key, int defaultValue, string sharedName)
                        => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        double IPreferences.Get(string key, double defaultValue, string sharedName)
                           => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        float IPreferences.Get(string key, float defaultValue, string sharedName)
                          => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        long IPreferences.Get(string key, long defaultValue, string sharedName)
                         => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        void IPreferences.Set(string key, string value, string sharedName)
                         => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, bool value, string sharedName)
                         => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, int value, string sharedName)
                         => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, double value, string sharedName)
                         => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, float value, string sharedName)
                         => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, long value, string sharedName)
                         => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        DateTime IPreferences.Get(string key, DateTime defaultValue)
                             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        void IPreferences.Set(string key, DateTime value)
                         => Xamarin.Essentials.Preferences.Set(key, value);

        DateTime IPreferences.Get(string key, DateTime defaultValue, string sharedName)
                             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        void IPreferences.Set(string key, DateTime value, string sharedName)
                         => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        Task<string> ISecureStorage.GetAsync(string key)
                                   => Xamarin.Essentials.SecureStorage.GetAsync(key);

        Task ISecureStorage.SetAsync(string key, string value)
                           => Xamarin.Essentials.SecureStorage.SetAsync(key, value);

        bool ISecureStorage.Remove(string key)
                           => Xamarin.Essentials.SecureStorage.Remove(key);

        void ISecureStorage.RemoveAll()
                           => Xamarin.Essentials.SecureStorage.RemoveAll();

        Task ISms.ComposeAsync()
                 => Xamarin.Essentials.Sms.ComposeAsync();

        Task ISms.ComposeAsync(SmsMessage message)
                 => Xamarin.Essentials.Sms.ComposeAsync(message);

        Task<IEnumerable<Locale>> ITextToSpeech.GetLocalesAsync()
                                               => Xamarin.Essentials.TextToSpeech.GetLocalesAsync();

        Task ITextToSpeech.SpeakAsync(string text, CancellationToken cancellationToken = default)
                          => Xamarin.Essentials.TextToSpeech.SpeakAsync(text, cancellationToken);

        Task ITextToSpeech.SpeakAsync(string text, SpeechOptions options, CancellationToken cancellationToken = default)
                          => Xamarin.Essentials.TextToSpeech.SpeakAsync(text, options, cancellationToken);

        void IVersionTracking.Track()
                             => Xamarin.Essentials.VersionTracking.Track();

        bool IVersionTracking.IsFirstLaunchForVersion(string version)
                             => Xamarin.Essentials.VersionTracking.IsFirstLaunchForVersion(version);

        bool IVersionTracking.IsFirstLaunchForBuild(string build)
                             => Xamarin.Essentials.VersionTracking.IsFirstLaunchForBuild(build);

        bool IVersionTracking.IsFirstLaunchEver
                             => Xamarin.Essentials.VersionTracking.IsFirstLaunchEver;

        bool IVersionTracking.IsFirstLaunchForCurrentVersion
                             => Xamarin.Essentials.VersionTracking.IsFirstLaunchForCurrentVersion;

        bool IVersionTracking.IsFirstLaunchForCurrentBuild
                             => Xamarin.Essentials.VersionTracking.IsFirstLaunchForCurrentBuild;

        string IVersionTracking.CurrentVersion
                               => Xamarin.Essentials.VersionTracking.CurrentVersion;

        string IVersionTracking.CurrentBuild
                               => Xamarin.Essentials.VersionTracking.CurrentBuild;

        string IVersionTracking.PreviousVersion
                               => Xamarin.Essentials.VersionTracking.PreviousVersion;

        string IVersionTracking.PreviousBuild
                               => Xamarin.Essentials.VersionTracking.PreviousBuild;

        string IVersionTracking.FirstInstalledVersion
                               => Xamarin.Essentials.VersionTracking.FirstInstalledVersion;

        string IVersionTracking.FirstInstalledBuild
                               => Xamarin.Essentials.VersionTracking.FirstInstalledBuild;

        IEnumerable<string> IVersionTracking.VersionHistory
                                            => Xamarin.Essentials.VersionTracking.VersionHistory;

        IEnumerable<string> IVersionTracking.BuildHistory
                                            => Xamarin.Essentials.VersionTracking.BuildHistory;

        void IVibration.Vibrate()
                       => Xamarin.Essentials.Vibration.Vibrate();

        void IVibration.Vibrate(double duration)
                       => Xamarin.Essentials.Vibration.Vibrate(duration);

        void IVibration.Vibrate(TimeSpan duration)
                       => Xamarin.Essentials.Vibration.Vibrate(duration);

        void IVibration.Cancel()
                       => Xamarin.Essentials.Vibration.Cancel();
    }
#pragma warning restore CS1066

}