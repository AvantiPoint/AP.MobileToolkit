using System;

namespace AP.CrossPlatform.Core.Tests.Mocks
{
    [TestMock]
    public enum EnumerationMock
    {
        [EnumField("Field A")]
        FieldA,

        [EnumFooField]
        [EnumField("Field B")]
        FieldB
    }
}
