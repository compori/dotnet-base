using System;
using System.Collections.Generic;
using System.Text;

namespace Compori.ServiceTask
{
    /// <summary>
    /// Class ServiceSettings.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public class ServiceSettings : ICloneable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service instance is repeatable.
        /// </summary>
        /// <value><c>true</c> if this service instance is repeatable; otherwise, <c>false</c>.</value>
        public bool IsRepeatable { get; set; }

        /// <summary>
        /// Gets or sets the delay timespan starting the next execution if service is repeatable.
        /// </summary>
        /// <value>The timespan delay running next execution.</value>
        public TimeSpan DelayNextExecution { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Gets or sets the maximum executions.
        /// </summary>
        /// <value>The maximum executions.</value>
        public long MaximumExecutions { get; set; } = -1;

        /// <summary>
        /// Gets or sets a value indicating whether [retry on error].
        /// </summary>
        /// <value><c>true</c> if [retry on error]; otherwise, <c>false</c>.</value>
        public bool RetryOnError { get; set; }

        /// <summary>
        /// Gets or sets the delay on error.
        /// </summary>
        /// <value>The delay on error.</value>
        public TimeSpan DelayError { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>ServiceSettings.</returns>
        public ServiceSettings Clone()
        {
            return new ServiceSettings
            {
                Name = this.Name?.Clone() as string,
                IsRepeatable = this.IsRepeatable,
                DelayNextExecution = this.DelayNextExecution,
                MaximumExecutions = this.MaximumExecutions,
                RetryOnError = this.RetryOnError,
                DelayError = this.DelayError
            };
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
