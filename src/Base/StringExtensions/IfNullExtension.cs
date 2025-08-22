namespace Compori.StringExtensions
{
    /// <summary>
    /// String Extensions class for handling null values
    /// </summary>
    public static class IfNullExtension
    {
        /// <summary>
        /// If string is null, then the parameter <paramref name="nullValue"/> will be returned.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="nullValue">The null value.</param>
        /// <returns>System.String.</returns>
        public static string IfNull(this string value, string nullValue)
        {
            return value != null ? value : nullValue;
        }

        /// <summary>
        /// If string is null, then an empty string will be returned.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string IfNullThenEmpty(this string value)
        {
            return value.IfNull(string.Empty);
        }
    }
}
