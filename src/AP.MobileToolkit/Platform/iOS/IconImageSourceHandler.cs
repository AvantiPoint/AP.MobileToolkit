using AP.MobileToolkit.Fonts;
using Foundation;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using RectangleF = CoreGraphics.CGRect;

[assembly: ExportImageSourceHandler(typeof(IconImageSource), typeof(AP.MobileToolkit.Platform.IconImageSourceHandler))]
namespace AP.MobileToolkit.Platform
{
    internal class IconImageSourceHandler : IImageSourceHandler
    {
        public Task<UIImage> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default, float scale = 1) =>
            new Task<UIImage>(() => LoadImage(imagesource, cancelationToken, scale));

        private UIImage LoadImage(ImageSource imagesource, CancellationToken cancelationToken = default, float scale = 1)
        {
            UIImage image = null;

            if (imagesource is IconImageSource iconsource && IconFontRegistry.Instance.TryFindIconForKey(iconsource.Name, out var icon))
            {
                var iconcolor = iconsource.Color != Color.Default ? iconsource.Color : Color.White;
                var imagesize = new SizeF((float)iconsource.Size * scale, (float)iconsource.Size * scale);
                var font = UIFont.FromName(icon.FontFamily ?? string.Empty, (float)iconsource.Size) ??
                    UIFont.SystemFontOfSize((float)iconsource.Size);

                UIGraphics.BeginImageContextWithOptions(imagesize, false, 0f);
                var attString = new NSAttributedString($"{icon.Glyph}", font: font, foregroundColor: iconcolor.ToUIColor());

                var ctx = new NSStringDrawingContext();
                var boundingRect = attString.GetBoundingRect(imagesize, 0, ctx);

                attString.DrawString(new RectangleF(
                    imagesize.Width / 2 - boundingRect.Size.Width / 2,
                    imagesize.Height / 2 - boundingRect.Size.Height / 2,
                    imagesize.Width,
                    imagesize.Height));

                image = UIGraphics.GetImageFromCurrentImageContext();

                UIGraphics.EndImageContext();

                if (iconcolor != Color.Default)
                    image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            }

            return image;
        }
    }
}
