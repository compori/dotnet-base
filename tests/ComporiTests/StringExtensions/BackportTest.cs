using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ComporiTest.StringExtensions
{
    using Compori.StringExtensions;

    public class BackportTest
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
