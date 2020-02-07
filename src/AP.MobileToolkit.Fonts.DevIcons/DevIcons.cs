using System.Runtime.CompilerServices;
using Xamarin.Forms;

[assembly: InternalsVisibleTo("AP.MobileToolkit.Tests")]
[assembly: ExportFont(AP.MobileToolkit.Fonts.DevIcons.FontName)]
namespace AP.MobileToolkit.Fonts
{
    public static class DevIcons
    {
        internal const string FontName = "devicons.ttf";

        public const string Prefix = "devicons";

        public const string Version = "1.8.0";

        public static readonly IFont Font = new EmbeddedWebFont(FontName, Prefix, "devicons.min.css", typeof(DevIcons));
    }
}
