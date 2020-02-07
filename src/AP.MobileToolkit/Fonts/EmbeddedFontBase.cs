using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AP.MobileToolkit.Fonts
{
    public abstract class EmbeddedFontBase : FontBase
    {
        protected EmbeddedFontBase(string fontFileName, string alias, Assembly assembly)
            : base(fontFileName, alias)
        {
            Assembly = assembly;
        }

        protected Assembly Assembly { get; }

        protected Stream GetResourceStream(string resourceName)
        {
            var resourceId = Assembly.GetManifestResourceNames()
                .FirstOrDefault(x => x.Equals(resourceName, StringComparison.InvariantCultureIgnoreCase) || x.EndsWith(resourceName, StringComparison.InvariantCultureIgnoreCase));

            return string.IsNullOrEmpty(resourceId) ? Stream.Null : Assembly.GetManifestResourceStream(resourceId);
        }
    }
}
