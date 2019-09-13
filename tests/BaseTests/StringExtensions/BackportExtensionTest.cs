using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ComporiTests.StringExtensions
{
    using Compori.StringExtensions;

    public class BackportExtensionTest
    {
        [Fact()]
        public void IsNullOrWhiteSpaceTest()
        {
            string sut;

            sut = null;
            Assert.True(sut.IsNullOrWhiteSpace());
            sut = "";
            Assert.True(sut.IsNullOrWhiteSpace());
            sut = "   ";
            Assert.True(sut.IsNullOrWhiteSpace());
            sut = " a  ";
            Assert.False(sut.IsNullOrWhiteSpace());
        }
    }
}
