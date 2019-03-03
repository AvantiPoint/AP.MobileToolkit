using System;
namespace AP.MobileToolkit
{
    internal static class Constants
    {
        internal const string ResolutionGroupName = "AvantiPoint";

        internal const string ImageEntryEffect = nameof(ImageEntryEffect);

        internal const string DisableAutoCorrectEffect = nameof(DisableAutoCorrectEffect);

        internal static string GetEffectName(string effect) =>
            $"{ResolutionGroupName}.{effect}";
    }
}
