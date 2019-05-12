using System;

namespace AP.CrossPlatform.Core.Tests.Mocks
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumFooFieldAttribute : Attribute
    {
        public EnumFooFieldAttribute()
        {
        }
    }
}
