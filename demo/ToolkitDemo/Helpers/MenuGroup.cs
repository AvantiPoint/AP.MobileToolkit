using System.ComponentModel;

namespace ToolkitDemo.Helpers
{
    internal enum MenuGroup
    {
        [Description("User Controls")]
        Controls,
        [Description("Converters")]
        Converters,
        [Description("API Client")]
        ApiClient,
    }
}
