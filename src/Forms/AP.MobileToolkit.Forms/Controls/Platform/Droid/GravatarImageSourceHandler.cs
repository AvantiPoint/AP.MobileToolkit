using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.Droid;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportImageSourceHandler(typeof(GravatarImageSource), typeof(GravatarImageSourceHandler))]
namespace AP.MobileToolkit.Controls.Platform.Droid
{
    public class GravatarImageSourceHandler : IImageSourceHandler
    {
        public async Task<Bitmap> LoadImageAsync(ImageSource imagesource, Context context, CancellationToken cancelationToken = default)
        {
            if (imagesource is GravatarImageSource gis)
            {
                var cacheFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, gis.CacheFileName);
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
                    return BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return null;
        }
    }
}
