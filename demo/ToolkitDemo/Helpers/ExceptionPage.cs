using System;
using Humanizer;
using Xamarin.Forms;

namespace ToolkitDemo.Helpers
{
    public static class ExceptionPage
    {
        public static Page ToErrorPage(this Exception ex) =>
            new ContentPage
            {
                Title = ex.GetType().Name.Humanize(),
                Content = new ScrollView
                {
                    Margin = new Thickness(20),
                    Content = GetMainLayout(ex, new StackLayout())
                }
            };

        private static View GetMainLayout(Exception ex, StackLayout sl)
        {
            sl.Children.Add(new Label { Text = $"{ex.GetType().Name}:", FontAttributes = FontAttributes.Bold });
            sl.Children.Add(new Label { Text = ex.Message });
            sl.Children.Add(new Label { Text = "Stack Trace:", FontAttributes = FontAttributes.Bold });
            sl.Children.Add(new Label { Text = ex.StackTrace });

            if (ex.InnerException != null)
            {
                sl.Children.Add(new BoxView { Color = Color.Black, HeightRequest = 1, Margin = new Thickness(20, 10) });
                return GetMainLayout(ex.InnerException, sl);
            }

            return sl;
        }
    }
}
