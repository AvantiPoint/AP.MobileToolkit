using System.ComponentModel;
using AP.MobileToolkit.Fonts.Mappings;

namespace AP.MobileToolkit.Fonts
{
    public static class FontAwesome
    {
        public static IconFont Regular => new IconFont("Font-Awesome-5-Free-Regular", "far", typeof(FontAwesomeRegular));
        public static IconFont Solid => new IconFont("Font-Awesome-5-Free-Solid", "fas", typeof(FontAwesomeSolid));
        public static IconFont Brands => new IconFont("Font-Awesome-5-Brands-Regular", "fab", typeof(FontAwesomeBrands));
        public const string Version = "5.11.2";
    }
}
