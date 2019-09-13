using System;

namespace Compori
{
    /// <summary>
    /// The interface IDisposalState extends <see cref="IDisposable"/> and provides a state,
    /// in order to check if the object is already disposed. 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDisposalState : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value>
        bool IsDisposed
        {
            get;
        }
    }
}
