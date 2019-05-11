using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.Droid;
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
                var imageBytes = await gis.GetGravatarAsync();

                if (imageBytes.Length > 0)
                {
                    return BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return null;
        }
    }
}
