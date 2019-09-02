using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compori.StringExtensions
{
    /// <summary>
    /// String Extensions class for handling cuts values
    /// </summary>
    public static class Cutter
    {
        /// <summary>
        /// Cuts the string at specified length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string Cut(this string value, int length)
        {
            if (value != null && value.Length > length)
            {
                Guard.AssertArgumentIsInRange(length, nameof(length), v => v >= 0, "Length cannot be less than zero.");
                return value.Substring(0, length);
            }
            return value;
        }
    }
}
