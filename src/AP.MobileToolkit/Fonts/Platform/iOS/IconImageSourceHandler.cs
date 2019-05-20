using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Color = Xamarin.Forms.Color;
using RectangleF = CoreGraphics.CGRect;

#pragma warning disable SA1300
namespace AP.MobileToolkit.Fonts.Platform.iOS
#pragma warning restore SA1300
{
    internal class IconImageSourceHandler : IImageSourceHandler
    {
        // should this be the default color on the BP for iOS?
        readonly Color _defaultColor = Color.White;

        public Task<UIImage> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default, float scale = 1)
        {
            UIImage image = null;
            if (imagesource is IconImageSource iconsource && IconFontRegistry.Instance.TryFindIconForKey(iconsource.Name, out var icon))
            {
                var iconcolor = iconsource.Color.IsDefault ? _defaultColor : iconsource.Color;
                var imagesize = new SizeF((float)iconsource.Size, (float)iconsource.Size);
                var font = UIFont.FromName(icon.FontFamily ?? string.Empty, (float)iconsource.Size) ??
                    UIFont.SystemFontOfSize((float)iconsource.Size);

                UIGraphics.BeginImageContextWithOptions(imagesize, false, 0f);
                var attString = new NSAttributedString(icon.Glyph, font: font, foregroundColor: iconcolor.ToUIColor());
                var ctx = new NSStringDrawingContext();
                var boundingRect = attString.GetBoundingRect(imagesize, (NSStringDrawingOptions)0, ctx);
                attString.DrawString(new RectangleF(
                    (imagesize.Width / 2) - (boundingRect.Size.Width / 2),
                    (imagesize.Height / 2) - (boundingRect.Size.Height / 2),
                    imagesize.Width,
                    imagesize.Height));
                image = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();

                if (iconcolor != _defaultColor)
                    image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            }
            return Task.FromResult(image);
        }
    }
}
