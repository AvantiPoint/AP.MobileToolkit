using System.ComponentModel;
using AP.MobileToolkit.Fonts.Mappings;
using Xamarin.Forms;

[assembly: ExportFont(AP.MobileToolkit.Fonts.FontAwesome.FontAwesomeBrandsFile)]
[assembly: ExportFont(AP.MobileToolkit.Fonts.FontAwesome.FontAwesomeRegularFile)]
[assembly: ExportFont(AP.MobileToolkit.Fonts.FontAwesome.FontAwesomeSolidFile)]
[assembly: XmlnsPrefix("http://avantipoint.com/schemas/xaml", "ap")]
[assembly: XmlnsDefinition("http://avantipoint.com/schemas/xaml", "AP.MobileToolkit.Fonts.Mappings")]
namespace AP.MobileToolkit.Fonts
{
    public static class FontAwesome
    {
        internal const string FontAwesomeRegularFile = "fa-regular-400.ttf";
        internal const string FontAwesomeSolidFile = "fa-solid-900.ttf";
        internal const string FontAwesomeBrandsFile = "fa-brands-400.ttf";

        public static IconFont Regular => new IconFont(FontAwesomeRegularFile, "far", typeof(FontAwesomeRegular));
        public static IconFont Solid => new IconFont(FontAwesomeSolidFile, "fas", typeof(FontAwesomeSolid));
        public static IconFont Brands => new IconFont(FontAwesomeBrandsFile, "fab", typeof(FontAwesomeBrands));

        public const string Version = "5.12.0";
    }
}
