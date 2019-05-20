using System.ComponentModel;
using AP.MobileToolkit.Fonts;
using Xamarin.Forms;

namespace AP.MobileToolkit.Menus
{
    public class MainMenuOption
    {
        public MainMenuOption()
        {
        }

        public string Text { get; set; }

        public string Glyph { get; set; }

        public string FontFamily { get; set; }

        public string Uri { get; set; }

        public int Priority { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageSource IconSource
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FontFamily))
                {
                    return new FontImageSource
                    {
                        Glyph = Glyph,
                        FontFamily = FontFamily,
                        Color = Color.White,
                        Size = 20
                    };
                }

                return new IconImageSource
                {
                    Name = Glyph,
                    Color = Color.White,
                    Size = 20
                };
            }
        }
    }
}
