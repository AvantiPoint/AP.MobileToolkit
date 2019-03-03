using System.Linq;
using AP.CrossPlatform.Extensions;
using AP.CrossPlatform.Core.Tests.Mocks;
using Xunit;
using Xunit.Abstractions;

namespace AP.CrossPlatform.Core.Tests.Fixtures.Extensions
{
    public class EnumExtensionsFixture
    {
        ITestOutputHelper _output { get; }

        public EnumExtensionsFixture(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GetAttribute_OnFieldWithoutAttribute()
        {
            EnumerationMock mock = EnumerationMock.FieldA;
            var attr = mock.GetAttribute<EnumFooFieldAttribute>();
            Assert.Null( attr );
        }

        [Fact]
        public void GetAttribute_OnFieldWithAttribute()
        {
            EnumerationMock mock = EnumerationMock.FieldB;
            var attr = mock.GetAttribute<EnumFieldAttribute>();
            Assert.IsType<EnumFieldAttribute>( attr );
            Assert.NotNull( attr );
            Assert.Equal( "Field B", attr.Value );
        }

        [Fact]
        public void GetAttributes_AllAttributes()
        {
            EnumerationMock mock = EnumerationMock.FieldB;
            var attrs = mock.GetAttributes();

            Assert.NotNull( attrs );
            Assert.Equal( 2, attrs.Count() );
            Assert.Contains( attrs, attr => attr.GetType() == typeof( EnumFieldAttribute ) );
            Assert.Contains( attrs, attr => attr.GetType() == typeof( EnumFooFieldAttribute ) );
        }

        [Fact]
        public void GetAttributes_OfGenericType()
        {
            EnumerationMock mock = EnumerationMock.FieldB;
            var attrs = mock.GetAttributes<EnumFieldAttribute>();

            foreach( var foo in attrs )
                _output.WriteLine( $"Attribute Value: {foo.Value} " );

            Assert.NotNull(attrs);
            Assert.Single(attrs);

            var attr = attrs.FirstOrDefault();

            Assert.NotNull(attr);
            Assert.Equal("Field B", attr.Value);
        }
    }
}
