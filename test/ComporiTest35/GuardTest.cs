using Compori;
using System;
using Xunit;

namespace ComporiTest35
{
    public class GuardTest
    {
        [Fact]
        public void TestAssertArgumentIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, null));
        }
    }
}
