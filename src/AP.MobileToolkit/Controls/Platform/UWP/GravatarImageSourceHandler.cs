using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Essentials;
using Xamarin.Forms.Platform.UWP;

namespace AP.MobileToolkit.Controls.Platform.UWP
{
    public class GravatarImageSourceHandler : IImageSourceHandler
    {
        public async Task<Windows.UI.Xaml.Media.ImageSource> LoadImageAsync(Xamarin.Forms.ImageSource imagesource, CancellationToken cancellationToken = default)
        {
            if (imagesource is GravatarImageSource gis)
            {
                var cacheFilePath = Path.Combine(FileSystem.CacheDirectory, gis.CacheFileName);
                var cacheFileInfo = new FileInfo(cacheFilePath);
                byte[] imageBytes;
                if (cacheFileInfo.Exists && cacheFileInfo.CreationTime.AddDays(7) < DateTime.Now)
                {
                    imageBytes = File.ReadAllBytes(cacheFilePath);
                }
                else
                {
                    // Delete Cached File
                    if (cacheFileInfo.Exists)
                    {
                        cacheFileInfo.Delete();
                    }

                    imageBytes = await gis.GetGravatarAsync();
                    File.WriteAllBytes(cacheFilePath, imageBytes);
                }

                if (imageBytes.Length > 0)
                {
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        var bitmapimage = new BitmapImage();
                        await bitmapimage.SetSourceAsync(stream.AsRandomAccessStream());
                        return bitmapimage;
                    }
                }
            }

            return null;
        }
    }
}
