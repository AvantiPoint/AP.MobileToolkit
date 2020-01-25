using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace AP.MobileToolkit.Fonts
{
    public sealed class IconFontRegistry
    {
        internal static IconFontRegistry Instance = new IconFontRegistry();

        internal List<IconFont> RegisteredFonts { get; }

        public static void Register(params IconFont[] fonts) =>
            RegisterInternal(fonts);

        public static void Register(IEnumerable<IconFont> fonts) =>
            RegisterInternal(fonts);

        private static void RegisterInternal(IEnumerable<IconFont> fonts)
        {
            foreach (var font in fonts)
            {
                if (Instance.RegisteredFonts.Any(x => x.Prefix.Equals(font.Prefix, StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new InvalidOperationException($"A font with the prefix '{font.Prefix}' has already been registered");
                }

                Instance.RegisteredFonts.Add(font);
            }
        }

        private IconFontRegistry()
        {
            RegisteredFonts = new List<IconFont>();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Reset()
        {
            Instance.RegisteredFonts.Clear();
            Instance = new IconFontRegistry();
        }

        internal IconInfo FindIconForKey(string iconName)
        {
            var prefix = iconName.Split('-')[0];
            var font = RegisteredFonts.FirstOrDefault(x => x.Prefix == prefix);

            if (string.IsNullOrEmpty(font.FontName))
            {
                return default;
            }

            return new IconInfo(font.FontName, font.FindIconForKey(iconName));
        }

        internal bool TryFindIconForKey(string iconName, out IconInfo icon)
        {
            icon = default;
            try
            {
                icon = FindIconForKey(iconName);
                return !string.IsNullOrEmpty(icon.FontFamily) && !string.IsNullOrEmpty(icon.Glyph);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
