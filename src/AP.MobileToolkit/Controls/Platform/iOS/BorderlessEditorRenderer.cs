using System.ComponentModel;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
#pragma warning disable SA1300
namespace AP.MobileToolkit.Controls.Platform.iOS
#pragma warning restore SA1300
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
        }
    }
}
