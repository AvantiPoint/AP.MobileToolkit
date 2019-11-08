using System;
using System.Collections.Generic;
using System.IO;
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

        public IconFont(string fontName, string prefix, Type mappingClass)
            : this(fontName, prefix, mappingClass, mappingClass)
        {
        }

        public IconFont(string fontName, string prefix, Type mappingClass, Type resolvingType)
        {
            FontName = fontName;
            Prefix = prefix;
            Icons = GetIcons(mappingClass);
            HasLoadedFont = false;
            FontFile = FontFile.FromString(fontName);
            FontFile.Assembly = resolvingType.Assembly;
        }

        public string FontName { get; }

        public string Prefix { get; }

        internal bool HasLoadedFont { get; set; }

        internal FontFile FontFile { get; }

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
