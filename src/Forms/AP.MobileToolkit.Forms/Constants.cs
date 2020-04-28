namespace AP.MobileToolkit
{
    internal static class Constants
    {
        internal const string ResolutionGroupName = "AvantiPoint";

        internal const string DisableAutoCorrectEffect = nameof(DisableAutoCorrectEffect);

        internal static string GetEffectName(string effect) =>
            $"{ResolutionGroupName}.{effect}";
    }
}
