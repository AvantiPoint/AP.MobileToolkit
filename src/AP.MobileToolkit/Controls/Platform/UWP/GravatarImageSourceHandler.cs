using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms.Platform.UWP;

namespace AP.MobileToolkit.Controls.Platform.UWP
{
    public class GravatarImageSourceHandler : IImageSourceHandler
    {
        public async Task<Windows.UI.Xaml.Media.ImageSource> LoadImageAsync(Xamarin.Forms.ImageSource imagesource, CancellationToken cancellationToken = default)
        {
            if (imagesource is GravatarImageSource gis)
            {
                var imageBytes = await gis.GetGravatarAsync();

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
