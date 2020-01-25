using System;
using Xamarin.Forms;

[assembly: ExportFont(AP.MobileToolkit.Fonts.DevIcons.FontName)]
namespace AP.MobileToolkit.Fonts
{
    public static class DevIcons
    {
        internal const string FontName = "devicons.ttf";

        public const string Prefix = "devicons";

        public const string Version = "1.8.0";

        public static readonly IconFont Font = new IconFont(FontName, Prefix, typeof(Mappings.DevIcons));
    }
}
