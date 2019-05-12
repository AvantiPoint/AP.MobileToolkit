using System.ComponentModel;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.iOS;
using AP.MobileToolkit.Markdown;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MarkdownTextView), typeof(MarkdownTextViewRenderer))]
#pragma warning disable SA1300
namespace AP.MobileToolkit.Controls.Platform.iOS
#pragma warning restore SA1300
{
    public class MarkdownTextViewRenderer : LabelRenderer
    {
        public MarkdownTextView TextView => (MarkdownTextView)Element;

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (!string.IsNullOrEmpty(TextView.Markdown))
            {
                Control.AttributedText = TextUtil.GetAttributedStringFromHtml(TextView.Markdown.GetHtmlFromMarkdown(true));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Markdown" && !string.IsNullOrEmpty(TextView.Markdown))
            {
                Control.AttributedText = TextUtil.GetAttributedStringFromHtml(TextView.Markdown.GetHtmlFromMarkdown(true));
            }
        }
    }
}
