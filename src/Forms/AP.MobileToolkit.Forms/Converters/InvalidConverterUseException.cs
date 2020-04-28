using System;
using System.Diagnostics.CodeAnalysis;

namespace AP.MobileToolkit.Converters
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "This is a special case exception that will only ever have a message.")]
    public sealed class InvalidConverterUseException : Exception
    {
        public InvalidConverterUseException(string message)
            : base(message)
        {
        }
    }
}
