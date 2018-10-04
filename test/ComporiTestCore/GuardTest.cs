using Compori;
using System;
using Xunit;

namespace ComporiTestCore
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
