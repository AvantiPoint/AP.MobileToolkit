namespace AP.MobileToolkit.Fonts
{
    public struct IconInfo
    {
        public IconInfo(string fontFamily, string glyph)
        {
            FontFamily = fontFamily;
            Glyph = glyph;
        }

        public string FontFamily { get; }

        public string Glyph { get; }
    }
}
