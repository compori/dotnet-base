using System.Linq;

namespace Compori.StringExtensions
{
    /// <summary>
    /// Class IndexOfExtension.
    /// </summary>
    public static class IndexOfExtension
    {
        /// <summary>
        /// Searching for the first occurence of a character with satisfying the condition that 
        /// every previous characters must be allowed explicitly.
        /// </summary>
        /// <param name="instance">The string instance</param>
        /// <param name="search">Searching character</param>
        /// <param name="allowedChars">Allowed characters</param>
        /// <returns>Position of search character</returns>
        public static int IndexOf(this string instance, char search, char[] allowedChars)
        {
            Guard.AssertArgumentIsNotNull(allowedChars, nameof(allowedChars));

            //
            // instance is null or empty.
            //
            if (instance == null)
            {
                return -1;
            }

            for (var i = 0; i < instance.Length; i++)
            {
                if (instance[i].Equals(search))
                {
                    return i;
                }
                if (!allowedChars.Contains(instance[i]))
                {
                    return -1;
                }
            }
            return -1;
        }
    }
}
