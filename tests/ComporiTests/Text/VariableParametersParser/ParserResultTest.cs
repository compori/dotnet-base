using Compori.Text.VariableParametersParser;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComporiTests.Text.VariableParametersParser
{
    public class ParserResultTest
    {

        [Fact()]
        public void GetValueStringTest()
        {
            Mock<IDictionary<string, string>> mock;
            string value;
            string key;
            string defaultValue;
            string expect;
            string actual;
            ParserResult sut;

            // Value found
            key = "key";
            value = null;
            expect = null;
            defaultValue = null;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(true);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value not found
            key = "key";
            value = null;
            expect = null;
            defaultValue = null;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(false);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, defaultValue);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Never());
        }

        [Fact()]
        public void GetValueIntegerTest()
        {
            Mock<IDictionary<string, string>> mock;
            string value;
            string key;
            int defaultValue;
            int expect;
            int actual;
            bool found;
            ParserResult sut;

            // Value found
            key = "key";
            value = "1";
            found = true;
            expect = 1;
            defaultValue = -1;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found but not parsable
            key = "key";
            value = "abx5";
            found = true;
            expect = -1;
            defaultValue = -1;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found but null
            key = "key";
            value = null;
            found = true;
            expect = -1;
            defaultValue = -1;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value not found
            key = "key";
            value = "1";
            found = false;
            expect = -2;
            defaultValue = -2;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, defaultValue);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Never());
        }

        [Fact()]
        public void GetValueDoubleTest()
        {
            Mock<IDictionary<string, string>> mock;
            string value;
            string key;
            double defaultValue;
            double expect;
            double actual;
            bool found;
            ParserResult sut;

            // Value found
            key = "key";
            value = "1";
            found = true;
            expect = 1;
            defaultValue = -1;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found but not parsable
            key = "key";
            value = "abx5";
            found = true;
            expect = -1;
            defaultValue = -1;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found but null
            key = "key";
            value = null;
            found = true;
            expect = -1;
            defaultValue = -1;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value not found
            key = "key";
            value = "1";
            found = false;
            expect = -2;
            defaultValue = -2;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, defaultValue);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Never());
        }


        [Fact()]
        public void GetValueBooleanTest()
        {
            Mock<IDictionary<string, string>> mock;
            string value;
            string key;
            bool defaultValue;
            bool expect;
            bool actual;
            bool found;
            ParserResult sut;

            // Value found "1"
            key = "key";
            value = "1";
            found = true;
            expect = true;
            defaultValue = false;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found "yes"
            key = "key";
            value = "yes";
            found = true;
            expect = true;
            defaultValue = false;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found "true"
            key = "key";
            value = "true";
            found = true;
            expect = true;
            defaultValue = false;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found "on"
            key = "key";
            value = "on";
            found = true;
            expect = true;
            defaultValue = false;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found "0"
            key = "key";
            value = "0";
            found = true;
            expect = false;
            defaultValue = true;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found "no"
            key = "key";
            value = "no";
            found = true;
            expect = false;
            defaultValue = true;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found "false"
            key = "key";
            value = "false";
            found = true;
            expect = false;
            defaultValue = true;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found "off"
            key = "key";
            value = "off";
            found = true;
            expect = false;
            defaultValue = true;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found but not parsable
            key = "key";
            value = "abx5";
            found = true;
            expect = true;
            defaultValue = true;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value found but null
            key = "key";
            value = null;
            found = true;
            expect = true;
            defaultValue = true;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Once());

            // Value not found
            key = "key";
            value = "1";
            found = false;
            expect = true;
            defaultValue =true;
            mock = new Mock<IDictionary<string, string>>();
            mock.Setup(dict => dict.ContainsKey(key)).Returns(found);
            mock.Setup(dict => dict[key]).Returns(value);
            sut = new ParserResult(mock.Object);
            actual = sut.GetValue(key, defaultValue);
            Assert.Equal(expect, defaultValue);
            mock.Verify(dict => dict.ContainsKey(key), Times.Once());
            mock.Verify(dict => dict[key], Times.Never());
        }

        [Fact()]
        public void EqualsTest()
        {
            ParserResult sut, sut2;
            IDictionary<string, string> data, data2;
            bool expect;


            expect = false;
            data = new Dictionary<string, string>();
            data2 = new Dictionary<string, string>();
            sut = new ParserResult(data);
            sut2 = null;
            Assert.Equal(expect, sut.Equals(sut2));

            expect = true;
            data = new Dictionary<string, string>();
            data2 = new Dictionary<string, string>();
            sut = new ParserResult(data);
            sut2 = new ParserResult(data2);
            Assert.Equal(expect, sut.Equals(sut2));

            expect = false;
            data = new Dictionary<string, string>() { { "", "" } };
            data2 = new Dictionary<string, string>();
            sut = new ParserResult(data);
            sut2 = new ParserResult(data2);
            Assert.Equal(expect, sut.Equals(sut2));

            expect = true;
            data = new Dictionary<string, string>() { { "", "" } };
            data2 = new Dictionary<string, string>() { { "", "" } };
            sut = new ParserResult(data);
            sut2 = new ParserResult(data2);
            Assert.Equal(expect, sut.Equals(sut2));

            expect = false;
            data = new Dictionary<string, string>() { { "1", "A" } };
            data2 = new Dictionary<string, string>() { { "1", "B" } };
            sut = new ParserResult(data);
            sut2 = new ParserResult(data2);
            Assert.Equal(expect, sut.Equals(sut2));

            expect = true;
            data = new Dictionary<string, string>() { { "1", "A" }, { "2", "B" } };
            data2 = new Dictionary<string, string>() { { "2", "B" }, { "1", "A" } };
            sut = new ParserResult(data);
            sut2 = new ParserResult(data2);
            Assert.Equal(expect, sut.Equals(sut2));
        }

#if !NET35

        [Fact()]
        public void GetHashCodeTest()
        {
            var rnd = new Random();
            Mock<IDictionary<string, string>> mock;
            int expect = 0;
            int actual = 0;
            ParserResult sut;

            // Testing Hashcode
            expect = rnd.Next();
            mock = new Mock<IDictionary<string, string>>(); 
            mock.Setup(dict => dict.GetHashCode()).Returns(expect);
            sut = new ParserResult(mock.Object);
            actual = sut.GetHashCode();
            Assert.Equal(expect, actual);
            actual = sut.GetHashCode();
            Assert.Equal(expect, actual);
            mock.Verify(dict => dict.GetHashCode(), Times.Exactly(2));
        }
#endif

    }
}
