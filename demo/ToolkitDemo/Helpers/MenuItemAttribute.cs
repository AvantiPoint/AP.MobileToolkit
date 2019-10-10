using System;
using System.ComponentModel;

namespace ToolkitDemo.Helpers
{
    internal enum MenuGroup
    {
        [Description("User Controls")]
        Controls,
        [Description("Converters")]
        Converters
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class MenuItemAttribute : Attribute
    {
        public MenuItemAttribute(string displayName, string navigationName, MenuGroup menuGroup)
        {
            DisplayName = displayName;
            NavigationName = navigationName;
            MenuGroup = menuGroup;
        }

        public string DisplayName { get; }

        public string NavigationName { get; }

        public MenuGroup MenuGroup { get; }
    }
}
