/*using System;
using System.IO;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Platform;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmbeddedFontLoader))]
namespace AP.MobileToolkit.Platform
{
    internal class EmbeddedFontLoader : IEmbeddedFontLoader
    {
        const string _fontCacheFolderName = "fonts";

        public (bool success, string filePath) LoadFont(FontFile font)
        {
            try
            {
                using (var stream = font.ResourceStream)
                {
                    var t = ApplicationData.Current.LocalFolder.CreateFolderAsync(_fontCacheFolderName, CreationCollisionOption.OpenIfExists);
                    var tmpdir = t.AsTask().Result;

                    var file = tmpdir.TryGetItemAsync(font.FileNameWithExtension()).AsTask().Result;
                    string filePath = string.Empty;
                    if (file != null)
                    {
                        filePath = file.Path;
                        return (true, CleanseFilePath(filePath));
                    }

                    try
                    {
                        var f = tmpdir.CreateFileAsync(font.FileNameWithExtension()).AsTask().Result;
                        filePath = f.Path;
                        using (var fileStream = File.Open(f.Path, FileMode.Open))
                        {
                            font.ResourceStream.CopyTo(fileStream);
                        }
                        return (true, CleanseFilePath(filePath));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        File.Delete(filePath);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return (false, null);
        }

        private static string CleanseFilePath(string filePath)
        {
            var fontName = Path.GetFileName(filePath);
            filePath = Path.Combine("local", _fontCacheFolderName, fontName);
            var baseUri = new Uri("ms-appdata://");
            var uri = new Uri(baseUri, filePath);
            var relativePath = uri.ToString().TrimEnd('/');
            return relativePath;
        }
    }
}*/
