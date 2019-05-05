using Android.Content;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.Droid;
using AP.MobileToolkit.Markdown;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MarkdownTextView), typeof(MarkdownTextViewRenderer))]
namespace AP.MobileToolkit.Controls.Platform.Droid
{
    public class MarkdownTextViewRenderer : LabelRenderer
    {
        public MarkdownTextViewRenderer(Context context) 
            : base(context)
        {
        }

        public MarkdownTextView TextView => (MarkdownTextView)Element;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Markdown" && !string.IsNullOrEmpty(TextView.Markdown))
            {
                Control.TextFormatted = TextUtil.GetFormattedHtml(TextView.Markdown.GetHtmlFromMarkdown(true));
            }
        }
    }
}
