using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Compori
{
    using StringExtensions;

    /// <summary>
    /// The Guard class provides methods for checking parameter values.
    /// The actual program code will be cleaner and easier to maintain.
    /// </summary>
    sealed public class Guard
    {

        /// <summary>
        /// Prevents a default instance of the <see cref="Guard"/> class from being created.
        /// </summary>
        private Guard()
        {
            throw new InvalidOperationException("This class cannot be instantiated.");
        }

        /// <summary>
        /// Assures that the parameter value is not null.
        /// If the assertion is violated, an <see cref="ArgumentNullException" /> will be is thrown.
        /// </summary>
        /// <param name="value">The value of the parameter to be checked.</param>
        /// <param name="argument">The name of the parameter to be checked.</param>
        /// <param name="message">A message that is passed as an exception message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNull(Object value, string argument, string message)
        {
            // Prüfe den Parametername
            if (argument.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(argument));
            }

            if (value == null)
            {
                if (message == null)
                {
                    throw new ArgumentNullException(argument);
                }
                throw new ArgumentNullException(argument, message);
            }
        }

        /// <summary>
        /// Assures that the parameter value is not null.
        /// If the assertion is violated, an <see cref="ArgumentNullException" /> will be is thrown.
        /// </summary>
        /// <param name="value">The value of the parameter to be checked.</param>
        /// <param name="argument">The name of the parameter to be checked.</param>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNull(object value, string argument)
        {
            Guard.AssertArgumentIsNotNull(value, argument, null);
        }

        /// <summary>
        /// Assures that the parameter value is not null or empty or contains only whitespaces.
        /// If the assertion is violated, an <see cref="ArgumentNullException" /> will be is thrown.
        /// </summary>
        /// <param name="value">The value of the parameter to be checked.</param>
        /// <param name="argument">The name of the parameter to be checked.</param>
        /// <param name="message">A message that is passed as an exception message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNullOrWhiteSpace(string value, string argument, string message)
        {
            // Check parameter "argument"
            if (argument.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(argument));
            }

            // Check value of "argument"
            if (value.IsNullOrWhiteSpace())
            {
                if (message == null)
                {
                    throw new ArgumentNullException(argument);
                }
                throw new ArgumentNullException(argument, message);
            }
        }

        /// <summary>
        /// Assures that the parameter value is not null or empty or contains only whitespaces.
        /// If the assertion is violated, an <see cref="ArgumentNullException" /> will be is thrown.
        /// </summary>
        /// <param name="value">The value of the parameter to be checked.</param>
        /// <param name="argument">The name of the parameter to be checked.</param>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNullOrWhiteSpace(string value, string argument)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(value, argument, null);
        }

        /// <summary>
        /// Assures that the parameter value is within a validity range that can be checked by a function.
        /// If the assurance is violated, then an <see cref="ArgumentOutOfRangeException" /> will be thrown.
        /// </summary>
        /// <typeparam name="T">Generic type of value</typeparam>
        /// <param name="value">The value of the parameter to be checked.</param>
        /// <param name="argument">The name of the parameter to be checked.</param>
        /// <param name="check">The validation function to be called.</param>
        /// <param name="message">A message that is passed as an exception message.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Specified argument was out of the range of valid values.
        /// </exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsInRange<T>(T value, string argument, Func<T, bool> check, string message)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(argument, nameof(argument));
            Guard.AssertArgumentIsNotNull(check, nameof(check));

            // Invoke check function
            if (!check(value))
            {
                if (message == null)
                {
                    throw new ArgumentOutOfRangeException(argument, value, "Specified argument was out of the range of valid values.");
                }

                throw new ArgumentOutOfRangeException(argument, value, message);
            }
        }

        /// <summary>
        /// Assures that the parameter value is within a validity range that can be checked by a function.
        /// If the assurance is violated, then an <see cref="ArgumentOutOfRangeException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">Generic type of value</typeparam>
        /// <param name="value">The value of the parameter to be checked.</param>
        /// <param name="argument">The name of the parameter to be checked.</param>
        /// <param name="check">The validation function to be called.</param>
        [DebuggerHidden()]
        public static void AssertArgumentIsInRange<T>(T value, string argument, Func<T, bool> check)
        {
            Guard.AssertArgumentIsInRange<T>(value, argument, check, null);
        }

        /// <summary>
        /// Asserts that the object is not disposed.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="message">A message that is passed as an exception message.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        [DebuggerHidden()]
        public static void AssertObjectIsNotDisposed(IDisposalState instance, string message)
        {
            Guard.AssertArgumentIsNotNull(instance, nameof(instance));

            if (instance.IsDisposed)
            {
                if (message == null)
                {
                    throw new ObjectDisposedException(instance.ToString());
                }

                throw new ObjectDisposedException(instance.ToString(), message);
            }
        }

        /// <summary>
        /// Asserts that the object is not disposed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        [DebuggerHidden()]
        public static void AssertObjectIsNotDisposed(IDisposalState value)
        {
            Guard.AssertObjectIsNotDisposed(value, null);
        }
    }
}
