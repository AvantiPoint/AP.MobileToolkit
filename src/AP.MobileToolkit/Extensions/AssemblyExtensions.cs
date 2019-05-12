using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AP.MobileToolkit.Extensions
{
    public static class AssemblyExtensions
    {
        public static bool IsDebug(this Assembly assembly) =>
            assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().Any(da => da.IsJITTrackingEnabled);
    }
}
