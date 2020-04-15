using System;
using System.Collections.Generic;
using System.Text;
using AP.CrossPlatform.Extensions;
using Xunit;

namespace AP.CrossPlatform.Core.Tests.Fixtures.Extensions
{
    public class StringExtensionFixture
    {
        private const string DoubleQuoteInput = "“Hello World”";
        private const string SingleQuoteInput = "‘Hello World’";

        [Fact]
        public void ReplacesDoubleQuotes()
        {
            Assert.True(DoubleQuoteInput.HasNonASCIIChars());

            var ascii = DoubleQuoteInput.ToASCII();
            Assert.NotEqual(DoubleQuoteInput, ascii);

            Assert.False(ascii.HasNonASCIIChars());
            Assert.Equal(DoubleQuoteInput.Length, ascii.Length);
            Assert.Equal("\"Hello World\"", ascii);
        }

        [Fact]
        public void ReplacesSingleQuotes()
        {
            Assert.True(SingleQuoteInput.HasNonASCIIChars());

            var ascii = SingleQuoteInput.ToASCII();
            Assert.NotEqual(SingleQuoteInput, ascii);

            Assert.False(ascii.HasNonASCIIChars());
            Assert.Equal(SingleQuoteInput.Length, ascii.Length);
            Assert.Equal("'Hello World'", ascii);
        }

        [Fact]
        public void ReplacesCommonFraction()
        {
            var input = "¾";
            Assert.True(input.HasNonASCIIChars());

            var ascii = input.ToASCII();

            Assert.False(ascii.HasNonASCIIChars());
            Assert.Equal(3, ascii.Length);

            Assert.Equal("3/4", ascii);
        }

        [Fact]
        public void ReplacesCopyrightSymbol()
        {
            var input = "© AvantiPoint";
            Assert.True(input.HasNonASCIIChars());

            var ascii = input.ToASCII();

            Assert.False(ascii.HasNonASCIIChars());

            Assert.Equal("(C) AvantiPoint", ascii);
        }
    }
}
