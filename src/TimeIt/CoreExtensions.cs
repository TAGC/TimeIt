using System;

namespace TimeItCore
{
    public static class CoreExtensions
    {
        public static IChainableDisposable<Setup> DoNothing(this Setup setup) => setup.Do(_ => {});

        public static IChainableDisposable<Setup> ThrowIfLongerThan(this Setup setup, int timeout) =>
            setup.ThrowIfLongerThan(TimeSpan.FromMilliseconds(timeout));

        public static IChainableDisposable<Setup> ThrowIfLongerThan(this Setup setup, TimeSpan timeout)
        {
            void Callback(TimeSpan elapsed)
            {
                if (elapsed > timeout)
                {
                    throw new TimeoutException($"Code executed in {elapsed} (exceeds timeout: {timeout}");
                }
            }

            return setup.Do(Callback);
        }
    }
}