using System.Linq;
using System.Reflection;

namespace AP.MobileToolkit.Fonts
{
    internal struct RegisteredIcon
    {
        public RegisteredIcon(FieldInfo fi)
        {
            PropertyName = fi.Name;
            Glyph = fi.GetValue(null).ToString();
            IconName = SanitizeName(fi);
        }

        /// <summary>
        /// Gets The Property Name reflects the C# class Field Name such as DownArrow
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets The Icon Name reflects what you would typically see in CSS such as down-arrow
        /// </summary>
        public string IconName { get; }

        /// <summary>
        /// Gets The Glyph Character
        /// </summary>
        public string Glyph { get; }

        private static string SanitizeName(FieldInfo fi)
        {
            string output = string.Empty;
            if (fi.GetCustomAttributes(typeof(IconNameAttribute), false).Any())
            {
                output = fi.GetCustomAttribute<IconNameAttribute>().Name;
            }
            else
            {
                for (var i = 0; i < fi.Name.Length; i++)
                {
                    var c = fi.Name[i];
                    if (char.IsUpper(c) && i > 0)
                    {
                        output += '-';
                    }
                    output += c;
                }
            }

            return output.ToLower();
        }
    }

    internal interface IEmbeddedFontLoader
    {
        (bool success, string filePath) LoadFont(FontFile font);
    }
}
