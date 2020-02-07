using System;
using System.Collections.Generic;
using System.Reflection;
using AP.MobileToolkit.Fonts.StyleSheets;

namespace AP.MobileToolkit.Fonts
{
    public class EmbeddedWebFont : EmbeddedFontBase
    {
        private ICssParser _cssParser;

        public EmbeddedWebFont(string fontFileName, string alias, string cssFileName, Type resolvingType)
            : this(fontFileName, alias, cssFileName, resolvingType.Assembly)
        {
        }

        public EmbeddedWebFont(string fontFileName, string alias, string cssFileName, Assembly assembly)
            : base(fontFileName, alias, assembly)
        {
            CssFileName = cssFileName;
            _glyphs = new Dictionary<string, string>();
        }

        private string CssFileName { get; }

        private Dictionary<string, string> _glyphs { get; }

        public override string GetGlyph(string name)
        {
            if (_glyphs.ContainsKey(name))
            {
                return _glyphs[name];
            }

            if (_cssParser is null)
            {
                _cssParser = new CssParser();
                _cssParser.ReadCSSFile(CssFileName, Assembly);
            }

            return _glyphs[name] = _cssParser.GetFontIcon(name);
        }
    }
}
