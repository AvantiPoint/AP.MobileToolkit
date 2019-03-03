using System;
using Xamarin.Forms;

namespace AP.MobileToolkit.Pages
{
    public static class CustomPage
    {
        public static readonly BindableProperty FormattedTitleProperty = 
            BindableProperty.CreateAttached("FormattedTitle", typeof(FormattedString), typeof(Page), null);

        public static FormattedString GetFormattedTitle(BindableObject view) =>
            (FormattedString)view.GetValue(FormattedTitleProperty);

        public static void SetFormattedTitle(BindableObject view, FormattedString value) =>
            view.SetValue(FormattedTitleProperty, value);

        public static readonly BindableProperty FormattedSubtitleProperty = 
            BindableProperty.CreateAttached("FormattedSubtitle", typeof(FormattedString), typeof(Page), null);

        public static FormattedString GetFormattedSubtitle(BindableObject view) =>
            (FormattedString)view.GetValue(FormattedSubtitleProperty);

        public static void SetFormattedSubtitle(BindableObject view, FormattedString value) =>
            view.SetValue(FormattedSubtitleProperty, value);

        public static readonly BindableProperty SubtitleProperty = 
            BindableProperty.CreateAttached("Subtitle", typeof(string), typeof(CustomPage), null);

        public static string GetSubtitle(BindableObject view) =>
            (string)view.GetValue(SubtitleProperty);

        public static void SetSubtitle(BindableObject view, string value) =>
            view.SetValue(SubtitleProperty, value);
    }
}