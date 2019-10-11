using System;

namespace ToolkitDemo.Helpers
{
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
