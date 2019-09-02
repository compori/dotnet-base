using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComporiTests.StringExtensions
{
    using Compori.StringExtensions;

    public class IndexOfExtensionTest
    {
        public static IEnumerable<object[]> IndexOfTestData
        {
            get
            {
                yield return new object[] { -1, null, 'a', new char[] { } };
                yield return new object[] { -1, "", '1', new char[] { '1', '2', '3' } };
                yield return new object[] { 0, "a", 'a', new char[] { } };
                yield return new object[] { -1, "b", 'a', new char[] { } };
                yield return new object[] { -1, "ba", 'a', new char[] { } };
                yield return new object[] { 3, "123a", 'a', new char[] { '1', '2', '3' } };
            }
        }

#if NET35

        [Fact()]
        public void IndexOfTest()
        {
            foreach(var data in IndexOfTestData)
            {
                Assert.Equal((int)data[0], (data[1] as string).IndexOf((char)data[2], (char[])data[3]));
            }
        }

#else

        [Theory(), MemberData(nameof(IndexOfTestData))]
        public void IndexOfTest(int expect, string value, char search, char[] allowed)
        {
            Assert.Equal(expect, value.IndexOf(search, allowed));
        }
#endif


    }
}
