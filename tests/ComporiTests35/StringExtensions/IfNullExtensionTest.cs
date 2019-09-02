using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComporiTests.StringExtensions
{
    using Compori.StringExtensions;

    public class IfNullExtensionTest
    {
        [Fact()]
        public void IfNullTest()
        {
            string sut;

            sut = null;
            Assert.Null(sut.IfNull(null));
            Assert.Equal("Is Null", sut.IfNull("Is Null"));

            sut = "";
            Assert.NotNull(sut.IfNull(null));
            Assert.NotEqual("Is Null", sut.IfNull("Is Null"));
            Assert.Equal(sut, sut.IfNull("Is Null"));

            sut = "ABC";
            Assert.NotNull(sut.IfNull(null));
            Assert.NotEqual("Is Null", sut.IfNull("Is Null"));
            Assert.Equal(sut, sut.IfNull("Is Null"));
        }

        [Fact()]
        public void IfNullThenEmptyTest()
        {
            string sut;

            sut = null;
            Assert.Empty(sut.IfNullThenEmpty());

            sut = "";
            Assert.Empty(sut.IfNullThenEmpty());

            sut = "ABC";
            Assert.NotNull(sut.IfNullThenEmpty());
            Assert.NotEmpty(sut.IfNullThenEmpty());
            Assert.Equal(sut, sut.IfNullThenEmpty());
        }
    }
}
