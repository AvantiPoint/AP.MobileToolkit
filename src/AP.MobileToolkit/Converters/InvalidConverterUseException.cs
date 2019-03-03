using System;
using System.Collections.Generic;
using System.Text;

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
