using System;
using System.Globalization;
using System.Resources;

namespace AP.CrossPlatform.i18n
{
    public interface ILocalize
    {
        string this[string key] { get; }
        string GetEnumString(Enum value);
        void RegisterManager(ResourceManager manager);
        void SetCulture(CultureInfo culture);
    }
}
