using System;
using System.IO;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Platform;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmbeddedFontLoader))]
namespace AP.MobileToolkit.Platform
{
    internal class EmbeddedFontLoader : IEmbeddedFontLoader
    {
        public (bool success, string filePath) LoadFont(FontFile font)
        {
            var tmpdir = Path.GetTempPath();
            var filePath = Path.Combine(tmpdir, font.FileNameWithExtension());
            if (File.Exists(filePath))
                return (true, filePath);
            try
            {
                using (var fileStream = File.Create(filePath))
                {
                    font.ResourceStream.CopyTo(fileStream);
                }

                return (true, filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                File.Delete(filePath);
            }

            return (false, null);
        }
    }
}
