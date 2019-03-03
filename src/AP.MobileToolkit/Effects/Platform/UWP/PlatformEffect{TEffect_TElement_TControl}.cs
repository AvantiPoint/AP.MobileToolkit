//using System.Linq;
//using Windows.UI.Xaml.Controls;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.UWP;

//namespace AP.MobileToolkit.Effects.Platform.UWP
//{
//    public abstract class PlatformEffect<TEffect, TElement, TControl> : PlatformEffect
//        where TEffect : RoutingEffect
//        where TElement : View
//        //where TControl : Control
//    {
//        protected new TElement Element
//        {
//            get => (TElement)base.Element;
//        }

//        protected TEffect Effect
//        {
//            get => (TEffect)Element?.Effects?.FirstOrDefault(e => e is TElement);
//        }

//        //protected new TControl Control
//        //{
//        //    get => (TControl)base.Control;
//        //}
//    }
//}