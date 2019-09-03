using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compori.Text.VariableParametersParser
{
    /// <summary>
    /// Class Parser.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Parses a list of string into a dictionary where keys are the string part before the '=' characters
        /// and values the following characters.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>ParserResult.</returns>
        public ParserResult Parse(List<string> values)
        {
            return this.Parse(values, false);
        }

        /// <summary>
        /// Parses a list of string into a dictionary where keys are the string part before the '=' characters
        /// and values the following characters.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="upperCaseKeys">if set to <c>true</c> keys will be store upper case.</param>
        /// <returns>ParserResult.</returns>
        public ParserResult Parse(List<string> values, bool upperCaseKeys)
        {
            return this.Parse(values, upperCaseKeys, false);
        }

        /// <summary>
        /// Parses a list of string into a dictionary where keys are the string part before the '=' characters
        /// and values the following characters.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="upperCaseKeys">if set to <c>true</c> keys will be store upper case.</param>
        /// <param name="trimQuotesInValues">if set to <c>true</c> the assigend value will be trimmed by single or double qoutes.</param>
        /// <returns>ParserResult.</returns>
        public ParserResult Parse(List<string> values, bool upperCaseKeys, bool trimQuotesInValues)
        {
            Guard.AssertArgumentIsNotNull(values, nameof(values));

            var result = new Dictionary<string, string>();
            foreach (var value in values)
            {
                if (value == null)
                {
                    continue;
                }

                // search for the first '=' character?
                var trimStartValue = value.TrimStart();
                var position = trimStartValue.IndexOf('=');

                var key = trimStartValue.TrimEnd();
                string keyValue = null;

                // If '=' character found.
                if (position >= 0)
                {
                    // determine the key and ...                    
                    key = trimStartValue
                        .Substring(0, position)
                        .TrimEnd();
                    
                    // the value
                    keyValue = trimStartValue
                        .Substring(position + 1);

                    var trimmedValue = keyValue.Trim();

                    // trim single or double quotes pairwise 
                    if (trimQuotesInValues && trimmedValue.Length >= 2)
                    {
                        if (trimmedValue.StartsWith("\"") && trimmedValue.EndsWith("\""))
                        {
                            keyValue = trimmedValue.Substring(1, trimmedValue.Length - 2);
                        }
                        if (trimmedValue.StartsWith("'") && trimmedValue.EndsWith("'"))
                        {
                            keyValue = trimmedValue.Substring(1, trimmedValue.Length - 2);
                        }
                    }
                }

                // Upper case key for case insensitive results
                if (upperCaseKeys)
                {
                    key = key.ToUpperInvariant();
                }

                // Add or replace value both
                if (result.ContainsKey(key))
                {
                    result.Remove(key);
                }
                result.Add(key, keyValue);
            }

            return new ParserResult(result);
        }
    }
}