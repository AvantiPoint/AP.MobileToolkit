using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace AP.MobileToolkit.Effects.Platform.iOS
{
    public abstract class PlatformEffect<TEffect, TElement, TControl> : PlatformEffect
        where TEffect : RoutingEffect
        where TElement : View
        where TControl : UIView
    {
        protected new TElement Element
        {
            get => (TElement)base.Element;
        }

        protected TEffect Effect
        {
            get => (TEffect)Element?.Effects?.FirstOrDefault(e => e is TElement);
        }

        protected new TControl Control
        {
            get => (TControl)base.Control;
        }
    }
}