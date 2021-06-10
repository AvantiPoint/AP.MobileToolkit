using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
#if MONOANDROID90
using Android.Support.V4.Content;
#else
using AndroidX.Core.Content;
#endif
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace AP.MobileToolkit.Controls.Platform.Droid
{
    public class ImageEntryRenderer : EntryRenderer
    {
        ImageEntry element;

        public ImageEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (ImageEntry)Element;

            var editText = Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                        break;
                    case ImageAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
                        break;
                }
            }
            editText.CompoundDrawablePadding = 25;

#if MONOANDROID90
            Control.Background.SetColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
#else
            var colorFilter = new BlendModeColorFilter(element.LineColor.ToAndroid(), BlendMode.SrcAtop);
            Control.Background.SetColorFilter(colorFilter);
#endif
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", Context.PackageName);
            var drawable = ContextCompat.GetDrawable(Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
        }
    }
}
