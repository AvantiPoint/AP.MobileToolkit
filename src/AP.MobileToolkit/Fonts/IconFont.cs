using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AP.MobileToolkit.Fonts
{
    public struct IconFont
    {
        public IconFont(string fontName, string prefix, Type mappingClass, bool debug = false)
        {
            FontName = fontName;
            Prefix = prefix;
            Icons = mappingClass.GetFields(BindingFlags.Static)
                                .Where(x => x.FieldType == typeof(string) || x.FieldType == typeof(char))
                                .Select(x => new RegisteredIcon(x));

            Debug = debug;
        }

        public string FontName { get; }

        public string Prefix { get; }

        internal bool Debug { get; set; }

        internal IEnumerable<RegisteredIcon> Icons { get; }

        public string FindIconForKey(string name)
        {
            var glyphName = Regex.Replace(name, $"^{Prefix}-", string.Empty);
            var icon = Icons.FirstOrDefault(x => x.PropertyName.Equals(glyphName, StringComparison.InvariantCultureIgnoreCase) ||
                                x.IconName.Equals(glyphName, StringComparison.InvariantCultureIgnoreCase));
            return icon.Glyph;
        }
    }
}
