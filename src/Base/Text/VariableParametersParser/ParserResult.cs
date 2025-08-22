using System;
using System.Collections.Generic;

namespace Compori.Text.VariableParametersParser
{
    /// <summary>
    /// Class ParserResult.
    /// Implements the <see cref="System.IEquatable{ParserResult}" />
    /// </summary>
    /// <seealso cref="System.IEquatable{ParserResult}" />
    public class ParserResult : IEquatable<ParserResult>
    {
        /// <summary>
        /// Stores the result of the parameter parser.
        /// </summary>
        /// <value>The values.</value>
        public IDictionary<string, string> Values { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserResult"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ParserResult(IDictionary<string, string> value)
        {
            Guard.AssertArgumentIsNotNull(value, nameof(value));

            this.Values = value;
        }

        /// <summary>
        /// Gets the string typed value for a key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string GetValue(string key, string defaultValue)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(key, nameof(key));

            return this.Values.ContainsKey(key) ? this.Values[key] : defaultValue;
        }

        /// <summary>
        /// Gets the int value for a key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public int GetValue(string key, int defaultValue)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(key, nameof(key));

            var lookupValue = this.Values.ContainsKey(key) ? this.Values[key] : null;

            if (lookupValue != null && int.TryParse(lookupValue, out int result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the double value for a key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public double GetValue(string key, double defaultValue)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(key, nameof(key));

            var lookupValue = this.Values.ContainsKey(key) ? this.Values[key] : null;

            if (lookupValue != null && double.TryParse(lookupValue, out double result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the boolean typed value for a key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool GetValue(string key, bool defaultValue)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(key, nameof(key));

            var lookupValue = this.Values.ContainsKey(key) ? this.Values[key] : null;

            if (lookupValue != null)
            {
                switch (lookupValue.ToUpperInvariant().Trim())
                {
                    case "1":
                    case "ON":
                    case "YES":
                    case "TRUE":
                        return true;

                    case "0":
                    case "OFF":
                    case "NO":
                    case "FALSE":
                        return false;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        protected virtual bool Equals(ParserResult other)
        {
            if (other == null)
            {
                return false;
            }

            if(this.Values.Count != other.Values.Count)
            {
                return false;
            }

            foreach(var kv in this.Values)
            {
                if (!other.Values.Contains(kv))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        bool IEquatable<ParserResult>.Equals(ParserResult other)
        {
            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with the current <see cref="object" />.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; 
        /// otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ParserResult);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms 
        /// and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return this.Values.GetHashCode();
        }
    }
}
