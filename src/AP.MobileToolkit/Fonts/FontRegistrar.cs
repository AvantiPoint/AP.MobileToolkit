﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AP.MobileToolkit.Fonts
{
    internal static class FontRegistrar
    {
        /*
        internal static readonly Dictionary<string, (ExportFontAttribute attribute, Assembly assembly)> EmbeddedFonts = new Dictionary<string, (ExportFontAttribute attribute, Assembly assembly)>();

        public static void Register(ExportFontAttribute fontAttribute, Assembly assembly)
        {
            EmbeddedFonts[fontAttribute.FontFileName] = (fontAttribute, assembly);
        }

        // TODO: Investigate making this Async
        //public static (bool hasFont, string fontPath) HasFont(string font)
        //{
        //    try
        //    {
        //        if (!EmbeddedFonts.TryGetValue(font, out var foundFont))
        //        {
        //            return (false, null);
        //        }

        //        var fontStream = GetEmbeddedResourceStream(foundFont.assembly, font);

        //        var type = Registrar.Registered.GetHandlerType(typeof(EmbeddedFont));
        //        var fontHandler = (IEmbeddedFontLoader)Activator.CreateInstance(type);
        //        return fontHandler.LoadFont(new EmbeddedFont { FontName = font, ResourceStream = fontStream });

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    return (false, null);
        //}

        //static Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
        //{
        //    var resourceNames = assembly.GetManifestResourceNames();

        //    var resourcePaths = resourceNames
        //        .Where(x => x.EndsWith(resourceFileName, StringComparison.CurrentCultureIgnoreCase))
        //        .ToArray();

        //    if (!resourcePaths.Any())
        //    {
        //        throw new Exception(string.Format("Resource ending with {0} not found.", resourceFileName));
        //    }
        //    if (resourcePaths.Length > 1)
        //    {
        //        resourcePaths = resourcePaths.Where(x => IsFile(x, resourceFileName)).ToArray();
        //    }

        //    return assembly.GetManifestResourceStream(resourcePaths.FirstOrDefault());
        //}

        //private static bool IsFile(string path, string file)
        //{
        //    if (!path.EndsWith(file, StringComparison.Ordinal))
        //        return false;
        //    return path.Replace(file, string.Empty).EndsWith(".", StringComparison.Ordinal);
        //}
        */
    }
}
