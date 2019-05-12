using System;
using Xamarin.Forms;

namespace AP.MobileToolkit.Extensions
{
    /// <summary>
    /// Bindable object extensions.
    /// </summary>
    public static class BindableObjectExtensions
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="bindable">Bindable.</param>
        /// <param name="property">Property.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetValue<T>(this BindableObject bindable, BindableProperty property) =>
            (T)bindable.GetValue(property);
    }
}
