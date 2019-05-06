using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AP.MobileToolkit.Extensions
{
    internal static class StringExtensions
    {
        public static string SanitizeViewModelTypeName(this Type type)
        {
            var name = Regex.Replace(type.Name, "(PageViewModel|ViewModel)$", "");

            if (type.Name == name) return name;

            var builder = new StringBuilder();
            foreach(char c in name)
            {
                if(char.IsUpper(c) && builder.Length > 0)
                {
                    builder.Append(" ");
                }

                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}
