using Compori.Text.VariableParametersParser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComporiTests.Text.VariableParametersParser
{
    public class ParserTest
    {
        public static IEnumerable<object[]> ParseTestData
        {
            get
            {
                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { } ),
                    new List<string>() { },
                    false,
                    false,
                };

                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "a", "b" }, } ),
                    new List<string>() { "a=b" },
                    false,
                    false,
                };

                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "a", " b" }, } ),
                    new List<string>() { "   a= b" },
                    false,
                    false,
                };

                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "a", " b" }, } ),
                    new List<string>() { "   a = b" },
                    false,
                    false,
                };

                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "a", "" }, } ),
                    new List<string>() { "a=" },
                    false,
                    false,
                };

                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> {
                        { "a", "simple" },
                        { "b", "abc" }
                    } ),
                    new List<string>() {
                        "a=", "b=abc", "a=simple"
                    },
                    false,
                    false,
                };
                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "a", null }, } ),
                    new List<string>() { "a" },
                    false,
                    false,
                };
                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "A", null }, } ),
                    new List<string>() { "a" },
                    true,
                    false,
                };

                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "A", "Simple value in Key \"A\"...  " }, } ),
                    new List<string>() { "a= \"Simple value in Key \"A\"...  \"  " },
                    true,
                    true,
                };
                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "A", "Simple value in Key \"A\"...  " }, } ),
                    new List<string>() { "a= 'Simple value in Key \"A\"...  '  " },
                    true,
                    true,
                };
                yield return new object[] {
                    new ParserResult( new Dictionary<string, string> { { "b", " \"Simple value in Key \"b\"...  '  " }, } ),
                    new List<string>() { "b= \"Simple value in Key \"b\"...  '  " },
                    false,
                    true,
                };
            }
        }

#if NET35

        [Fact()]
        public void ParseTest()
        {
            foreach(var data in ParseTestData)
            {
                this.AssertParseTestEqual((ParserResult)data[0], (List<string>)data[1], (bool)data[2], (bool)data[3]);
            }
        }

#else

        [Theory(), MemberData(nameof(ParseTestData))]
        public void ParseTest(ParserResult expect, List<string> parameters, bool upperCaseKeys, bool trimQuotes)
        {
            this.AssertParseTestEqual(expect, parameters, upperCaseKeys, trimQuotes);
        }

#endif
        protected void AssertParseTestEqual(ParserResult expect, List<string> parameters, bool upperCaseKeys, bool trimQuotes)
        {
            var sut = new Parser();
            if (!upperCaseKeys && !trimQuotes)
            {
                Assert.Equal(expect, sut.Parse(parameters));
            }
            if (!trimQuotes)
            {
                Assert.Equal(expect, sut.Parse(parameters, upperCaseKeys));
            }
            Assert.Equal(expect, sut.Parse(parameters, upperCaseKeys, trimQuotes));
        }
    }
}
