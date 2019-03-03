using System;
namespace AP.CrossPlatform.Core.Tests.Mocks
{
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Enum )]
    public class TestMockAttribute : Attribute
    {
        public TestMockAttribute()
        {
        }
    }
}
