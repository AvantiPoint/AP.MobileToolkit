using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Humanizer;

namespace AP.MobileToolkit.Fonts
{
    public partial struct IconFont
    {
        internal static IEnumerable<RegisteredIcon> GetIcons(Type mappingClass)
        {
            return mappingClass.GetFields(BindingFlags.Static | BindingFlags.Public)
                               .Where(x => x.FieldType == typeof(string) || x.FieldType == typeof(char))
                               .Select(x => new RegisteredIcon(x));
        }

        public IconFont(string fontName, string prefix, Type mappingClass, bool debug = false)
        {
            FontName = fontName;
            Prefix = prefix;
            Icons = GetIcons(mappingClass);
            Debug = debug;
        }

        public string FontName { get; }

        public string Prefix { get; }

        internal bool Debug { get; set; }

        internal IEnumerable<RegisteredIcon> Icons { get; }

        public string FindIconForKey(string name)
        {
            var glyphName = Regex.Replace(name, $"^{Prefix}-", string.Empty);
            glyphName = glyphName.Replace('-', ' ').Dehumanize();
            var icon = Icons.FirstOrDefault(x => x.PropertyName.Equals(glyphName, StringComparison.InvariantCultureIgnoreCase) ||
                                x.IconName.Equals(glyphName, StringComparison.InvariantCultureIgnoreCase));
            return icon.Glyph;
        }
    }
}
