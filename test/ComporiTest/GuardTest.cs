using Compori;
using System;
using Xunit;

namespace ComporiTest
{
    public class GuardTest
    {
        [Fact]
        public void TestAssertArgumentIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, null));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, ""));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull("", null));
        }
    }
}
