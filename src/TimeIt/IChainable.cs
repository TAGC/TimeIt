using System;

namespace TimeItCore
{
    public interface IChainableDisposable<out T> : IDisposable
    {
        T And { get; }
    }
}