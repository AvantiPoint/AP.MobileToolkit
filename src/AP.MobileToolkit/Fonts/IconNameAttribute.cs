using System;

namespace AP.MobileToolkit.Fonts
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class IconNameAttribute : Attribute
    {
        public IconNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
