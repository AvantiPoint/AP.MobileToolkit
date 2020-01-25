using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.iOS;
using Foundation;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportImageSourceHandler(typeof(GravatarImageSource), typeof(GravatarImageSourceHandler))]
#pragma warning disable SA1300
namespace AP.MobileToolkit.Controls.Platform.iOS
#pragma warning restore SA1300
{
    public class GravatarImageSourceHandler : IImageSourceHandler
    {
        public async Task<UIImage> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default, float scale = 1)
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
                    return UIImage.LoadFromData(NSData.FromArray(imageBytes));
                }
            }

            return null;
        }
    }
}
