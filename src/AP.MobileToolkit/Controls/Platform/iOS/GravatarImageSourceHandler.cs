using System.Threading;
using System.Threading.Tasks;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportImageSourceHandler(typeof(GravatarImageSource), typeof(GravatarImageSourceHandler))]
namespace AP.MobileToolkit.Controls.Platform.iOS
{
    public class GravatarImageSourceHandler : IImageSourceHandler
    {
        public async Task<UIImage> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default, float scale = 1)
        {
            if(imagesource is GravatarImageSource gis)
            {
                var imageBytes = await gis.GetGravatarAsync();

                if (imageBytes.Length > 0)
                {
                    return UIImage.LoadFromData(NSData.FromArray(imageBytes));
                }
            }

            return null;
        }
    }
}
