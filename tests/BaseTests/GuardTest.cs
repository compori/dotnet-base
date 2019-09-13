using Compori;
using System;
using Xunit;

namespace ComporiTest
{
    public class GuardTest
    {
        [Fact]
        public void TestConstructorFailed()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(() => (Guard)Activator.CreateInstance(typeof(Guard), true));
        }

        [Fact]
        public void AssertArgumentIsNotNullTest()
        {
            //
            // Invalid usage
            //
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, null));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, ""));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, "  "));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull("", null));

            //
            // Test usage
            //
            ArgumentNullException exception;

            // null
            exception = Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, "paramName"));
            Assert.Equal("paramName", exception.ParamName);

            // assertion fits
            Guard.AssertArgumentIsNotNull(1, "notNullParam");

            //
            // test custom message
            //
            exception = Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNull(null, "paramName", "My Message"));
            Assert.Equal("paramName", exception.ParamName);
#if NET35
            Assert.True(exception.Message.StartsWith("My Message"));
#else
            Assert.StartsWith("My Message", exception.Message);
#endif
        }

        [Fact]
        public void AssertArgumentIsNotNullOrWhiteSpaceTest()
        {
            //
            // Invalid usage
            //
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace(null, null));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace(null, ""));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace(null, "  "));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace("", null));

            //
            // Test usage
            //
            ArgumentNullException exception;

            // null
            exception = Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace(null, "paramName"));
            Assert.Equal("paramName", exception.ParamName);

            // empty
            exception = Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace("", "paramNameEmpty"));
            Assert.Equal("paramNameEmpty", exception.ParamName);

            // spaces
            exception = Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace("    ", "paramNameSpaces"));
            Assert.Equal("paramNameSpaces", exception.ParamName);

            // tab
            exception = Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace("\t", "paramNameTab"));
            Assert.Equal("paramNameTab", exception.ParamName);

            // assertion fits
            Guard.AssertArgumentIsNotNullOrWhiteSpace("some", "notNullParam");

            //
            // test custom message
            //
            exception = Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsNotNullOrWhiteSpace(null, "paramName", "My Message"));
            Assert.Equal("paramName", exception.ParamName);
#if NET35
            Assert.True(exception.Message.StartsWith("My Message"));
#else
            Assert.StartsWith("My Message", exception.Message);
#endif
        }

        [Fact]
        public void AssertArgumentIsInRangeTest()
        {
            //
            // Invalid usage
            //
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, null, null));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, "", null));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, "  ", null));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, null, null));

            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, null, v => v > 0));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, "", v => v > 0));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, "  ", v => v > 0));
            Assert.Throws<ArgumentNullException>(() => Guard.AssertArgumentIsInRange(0, null, v => v > 0));

            //
            // Test usage
            //
            ArgumentOutOfRangeException exception;

            // 0
            exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.AssertArgumentIsInRange(1, "paramName", v => v < 1));
            Assert.Equal(1, exception.ActualValue);
            Assert.Equal("paramName", exception.ParamName);

            // assertion fits
            Guard.AssertArgumentIsInRange(1, "paramName", v => v > 0);

            //
            // test custom message
            //
            exception = Assert.Throws<ArgumentOutOfRangeException>(() => Guard.AssertArgumentIsInRange(1, "paramName", v => v < 1, "My Message"));
            Assert.Equal(1, exception.ActualValue);
            Assert.Equal("paramName", exception.ParamName);
#if NET35
            Assert.True(exception.Message.StartsWith("My Message"));
#else
            Assert.StartsWith("My Message", exception.Message);
#endif

        }
    }
}
