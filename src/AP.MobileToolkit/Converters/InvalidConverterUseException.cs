using System;

namespace AP.MobileToolkit.Converters
{
    public sealed class InvalidConverterUseException : Exception
    {
        public InvalidConverterUseException(string message)
            : base(message)
        {
        }
    }
}
