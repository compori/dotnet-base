using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComporiTests.StringExtensions
{
    using Compori.StringExtensions;

    public class CropExtensionTest
    {
        [Fact()]
        public void CropTest()
        {
            string sut;

            sut = null;
            Assert.Null(sut.Crop(0));
            Assert.Null(sut.Crop(-1));

            sut = "";
            Assert.Empty(sut.Crop(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Assert.Empty(sut.Crop(-1)));
            Assert.Empty(sut.Crop(1));

            sut = "1";
            Assert.Empty(sut.Crop(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Assert.Empty(sut.Crop(-1)));
            Assert.Equal(sut, sut.Crop(10));

            sut = "abcdef";
            Assert.Empty(sut.Crop(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Assert.Empty(sut.Crop(-1)));
            Assert.Equal("ab", sut.Crop(2));
            Assert.Equal(sut, sut.Crop(10));

        }
    }
}
