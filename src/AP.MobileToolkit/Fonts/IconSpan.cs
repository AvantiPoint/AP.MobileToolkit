using Xamarin.Forms;

namespace AP.MobileToolkit.Fonts
{
    public sealed class IconSpan : Span
    {
        public static readonly BindableProperty GlyphNameProperty =
            BindableProperty.Create(nameof(GlyphName), typeof(string), typeof(IconSpan), null, BindingMode.OneTime, propertyChanged: OnGlyphNameChanged);

        private static void OnGlyphNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is IconSpan span)
            {
                var icon = IconFontRegistry.Instance.FindIconForKey(span.GlyphName);
                span.FontFamily = icon.FontFamily;
                span.Text = icon.Glyph;
            }
        }

        public string GlyphName
        {
            get => (string)GetValue(GlyphNameProperty);
            set => SetValue(GlyphNameProperty, value);
        }
    }
}
