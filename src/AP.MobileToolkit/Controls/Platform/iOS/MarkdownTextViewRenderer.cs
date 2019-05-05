using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using AP.MobileToolkit.Controls.Platform.iOS;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Markdown;

[assembly: ExportRenderer(typeof(MarkdownTextView), typeof(MarkdownTextViewRenderer))]
namespace AP.MobileToolkit.Controls.Platform.iOS
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
