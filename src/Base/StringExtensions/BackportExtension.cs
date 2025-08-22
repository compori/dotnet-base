namespace Compori.StringExtensions
{
    /// <summary>
    /// String Extensions class for Backport
    /// </summary>
    public static class BackportExtension
    {
        /// <summary>
        /// Determines whether the string is null or whitespace.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if [is null or white space] [the specified instance]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string instance)
        {
#if NET35
            return string.IsNullOrEmpty(instance) || instance.Trim().Length == 0;
#else
            return string.IsNullOrWhiteSpace(instance);
#endif
        }
    }
}
