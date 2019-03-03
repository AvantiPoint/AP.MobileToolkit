using System.Reflection;

namespace AP.MobileToolkit.Fonts
{
    internal struct RegisteredIcon
    {
        public RegisteredIcon(FieldInfo fi)
        {
            PropertyName = fi.Name;
            Glyph = fi.GetValue(null).ToString();
            IconName = SanitizeName(fi.Name);
        }

        /// <summary>
        /// The Property Name reflects the C# class Field Name such as DownArrow
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// The Icon Name reflects what you would typically see in CSS such as down-arrow
        /// </summary>
        public string IconName { get; }

        /// <summary>
        /// The Glyph Character
        /// </summary>
        public string Glyph { get; }

        private static string SanitizeName(string name)
        {
            string output = "";
            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (char.IsUpper(c) && i > 0)
                {
                    output += '-';
                }
                output += c;
            }

            return output.ToLower();
        }
    }
}
