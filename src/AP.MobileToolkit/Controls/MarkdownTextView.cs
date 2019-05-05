using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    public class MarkdownTextView : Label
    {
        public static readonly BindableProperty MarkdownProperty = 
            BindableProperty.Create(nameof(Markdown), typeof(string), typeof(MarkdownTextView), null);

        public string Markdown
        {
            get => (string)GetValue(MarkdownProperty);
            set => SetValue(MarkdownProperty, value);
        }
    }
}
