using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComporiTest.StringExtensions
{
    using Compori.StringExtensions;

    public class CutterTest
    {
        [Fact()]
        public void CutTest()
        {
            string sut;

            sut = null;
            Assert.Null(sut.Cut(0));
            Assert.Null(sut.Cut(-1));

            sut = "";
            Assert.Empty(sut.Cut(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Assert.Empty(sut.Cut(-1)));
            Assert.Empty(sut.Cut(1));

            sut = "1";
            Assert.Empty(sut.Cut(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Assert.Empty(sut.Cut(-1)));
            Assert.Equal(sut, sut.Cut(10));

            sut = "abcdef";
            Assert.Empty(sut.Cut(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Assert.Empty(sut.Cut(-1)));
            Assert.Equal("ab", sut.Cut(2));
            Assert.Equal(sut, sut.Cut(10));

        }
    }
}
