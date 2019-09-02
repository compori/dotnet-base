using System;
using System.Collections.Generic;
using System.Text;

namespace Compori.StringExtensions
{
    /// <summary>
    /// String Extensions class for Backport
    /// </summary>
    public static class Backport
    {
        /// <summary>
        /// Determines whether the string is null or whitespace.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if [is null or white space] [the specified instance]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhiteSpace(this String instance)
        {
#if NET35
            return String.IsNullOrEmpty(instance) || instance.Trim().Length == 0;
#else
            return String.IsNullOrWhiteSpace(instance);
#endif
        }
    }
}
