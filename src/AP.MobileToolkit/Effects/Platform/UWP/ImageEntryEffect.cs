//using Plugin.Iconize;
//using Plugin.Iconize.UWP;
//using Xamarin.Forms;

//namespace AP.MobileToolkit.Effects.Platform.UWP
//{
//    public class ImageEntryEffect : PlatformEffect<Effects.ImageEntryEffect, Entry, TextBox>
//    {
//        protected override async void OnAttached()
//        {
//            if(!string.IsNullOrWhiteSpace(Effect.Icon) && Iconize.FindIconForKey(Effect.Icon) != null)
//            {
//                var icon = Iconize.FindIconForKey(Effect.Icon);
//                var module = Iconize.FindModuleOf(icon);
//                //var image = icon.ToImageSource(Effect.ImageHeight, Element.TextColor);
//            }
//            else if(Effect.ImageSource != null)
//            {
//                //var handler = GetImageHandler(Effect.ImageSource);
//                //var image = await handler.LoadImageAsync(Effect.ImageSource);
//            }
//        }

//        protected override void OnDetached()
//        {
//        }
//    }
//}