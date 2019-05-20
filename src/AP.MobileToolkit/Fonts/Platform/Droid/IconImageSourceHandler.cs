using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Util;
using AP.MobileToolkit.Fonts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportImageSourceHandler(typeof(IconImageSource), typeof(AP.MobileToolkit.Platform.IconImageSourceHandler))]
namespace AP.MobileToolkit.Platform
{
    internal class IconImageSourceHandler : IImageSourceHandler
    {
        public Task<Bitmap> LoadImageAsync(ImageSource imagesource, Context context, CancellationToken cancelationToken = default) =>
            new Task<Bitmap>(() => LoadImage(imagesource, context));

        private Bitmap LoadImage(ImageSource imagesource, Context context)
        {
            Bitmap image = null;
            if (imagesource is IconImageSource iconsource && IconFontRegistry.Instance.TryFindIconForKey(iconsource.Name, out var icon))
            {
                var paint = new Paint
                {
                    TextSize = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)iconsource.Size, context.Resources.DisplayMetrics),
                    Color = (iconsource.Color != Color.Default ? iconsource.Color : Color.White).ToAndroid(),
                    TextAlign = Paint.Align.Left,
                    AntiAlias = true,
                };

                using (var typeface = Typeface.CreateFromAsset(context.ApplicationContext.Assets, icon.FontFamily))
                    paint.SetTypeface(typeface);

                var width = (int)(paint.MeasureText(icon.Glyph) + .5f);
                var baseline = (int)(-paint.Ascent() + .5f);
                var height = (int)(baseline + paint.Descent() + .5f);
                image = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
                var canvas = new Canvas(image);
                canvas.DrawText(icon.Glyph, 0, baseline, paint);
            }

            return image;
        }
    }
}
