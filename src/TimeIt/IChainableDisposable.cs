using System;
using JetBrains.Annotations;

namespace TimeItCore
{
    /// <summary>
    /// Represents a disposable object that can be chained to return another object (which may be itself).
    /// </summary>
    /// <typeparam name="T">The type of object to chain to.</typeparam>
    [PublicAPI]
    public interface IChainableDisposable<out T> : IDisposable
    {
        /// <summary>
        /// Gets the object this instance is chained to.
        /// </summary>
        T And { get; }
    }
}