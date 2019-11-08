using System;
using System.Collections.Generic;
using System.Text;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Platform;
using CoreGraphics;
using CoreText;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmbeddedFontLoader))]
namespace AP.MobileToolkit.Platform
{
    internal class EmbeddedFontLoader : IEmbeddedFontLoader
    {
        public (bool success, string filePath) LoadFont(FontFile font)
        {
            try
            {
                var data = NSData.FromStream(font.ResourceStream);

                var provider = new CGDataProvider(data);
                var cGFont = CGFont.CreateFromProvider(provider);
                if (CTFontManager.RegisterGraphicsFont(cGFont, out var error))
                {
                    error.Dispose();
                    return (true, null);
                }

                Console.WriteLine(error.Description);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return (false, null);
        }
    }
}
