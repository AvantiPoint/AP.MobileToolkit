using Android.Text;
using AP.MobileToolkit.Markdown;
using Java.Lang;

namespace AP.MobileToolkit.Controls.Platform.Droid
{
    internal static class TextUtil
    {
        public static ICharSequence GetFormattedHtml(string htmlText)
        {
            try
            {
                ICharSequence html;
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
                {
                    html = Html.FromHtml(htmlText.ParseCodeTags(), FromHtmlOptions.ModeLegacy, null, new HtmlTagHandler()) as ICharSequence;
                }
                else
                {
                    // handle legacy builds
#pragma warning disable CS0618 // Obsolete API is called only when running on older devices where it is supported.
                    html = Html.FromHtml(htmlText.ParseCodeTags(), null, new HtmlTagHandler()) as ICharSequence;
#pragma warning disable CS0618
                }

                // this is required to get rid of the end two "\n" that android adds with Html.FromHtml
                // see: http://stackoverflow.com/questions/16585557/extra-padding-on-textview-with-html-contents for example
                while (html.CharAt(html.Length() - 1) == '\n')
                {
                    html = html.SubSequenceFormatted(0, html.Length() - 1);
                }

                return html;
            }
            catch
            {
                return null;
            }
        }
    }
}
