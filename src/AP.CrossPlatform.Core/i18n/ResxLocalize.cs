using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace AP.CrossPlatform.i18n
{
    public class ResxLocalize : ILocalize
    {
        public static bool Debug { get; set; }

        private static ILocalize _current;
        public static ILocalize Current => _current ?? new ResxLocalize();

        private readonly List<ResourceManager> _managers = new List<ResourceManager>();
        private CultureInfo _currentCulture = CultureInfo.CurrentUICulture;

        public ResxLocalize()
        {
            _current = this;
        }

        public string this[string key]
        {
            get
            {
                foreach (var manager in _managers)
                {
                    var str = manager.GetString(key, _currentCulture);
                    if (!string.IsNullOrEmpty(str))
                        return str;
                }

                if (Debug)
                    return $"MISSING {key}";

                return string.Empty;
            }
        }

        public string GetEnumString(Enum value)
        {
            // ie. MyEnum.MyValue1 - will look key named - MyEnumMyValue1
            var key = value.GetType().Name + value;
            return this[key];
        }

        public void RegisterManager(ResourceManager manager)
        {
            if (!_managers.Contains(manager))
                _managers.Add(manager);
        }

        public void SetCulture(CultureInfo culture) =>
            _currentCulture = culture;
    }
}
