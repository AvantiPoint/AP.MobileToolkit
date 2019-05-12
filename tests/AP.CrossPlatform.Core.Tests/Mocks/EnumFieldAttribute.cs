using System;

namespace AP.CrossPlatform.Core.Tests.Mocks
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumFieldAttribute : Attribute
    {
        public string Value { get; }

        public EnumFieldAttribute(string value)
        {
            Value = value;
        }
    }
}
