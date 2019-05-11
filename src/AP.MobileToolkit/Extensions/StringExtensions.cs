using System;
using System.Text.RegularExpressions;
using Humanizer;

namespace AP.MobileToolkit.Extensions
{
    internal static class StringExtensions
    {
        public static string SanitizeViewModelTypeName(this Type type)
        {
            var name = Regex.Replace(type.Name, "(PageViewModel|ViewModel)$", "");

            return name.Humanize(LetterCasing.Title);
        }
    }
}
