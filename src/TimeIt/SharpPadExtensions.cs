using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SharpPad;

#pragma warning disable 4014

namespace TimeItCore
{
    /// <summary>
    /// Extends <see cref="Setup" /> with SharpPad dumping functionality.
    /// <para></para>
    /// SharpPad is a third-party tool designed to bring LinqPad-like functionality to Visual Studio Code.
    /// This tool and further information about it is available
    /// <a href="https://marketplace.visualstudio.com/items?itemName=jmazouri.sharppad">here</a>.
    /// </summary>
    [PublicAPI]
    public static class SharpPadExtensions
    {
        /// <summary>
        /// Configures the elapsed execution time to be dumped to SharpPad.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <remarks>
        /// The dump to SharpPad will be executed asynchronously. There is no guarantee that this dump will have
        /// completed before further configured actions on <paramref name="setup"/> begin executing.
        /// </remarks>
        /// <example>
        /// <code>
        /// setup.DumpToSharpPad();
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> DumpToSharpPad(this Setup setup) =>
            setup.Do(elapsed => elapsed.Dump());

        /// <summary>
        /// Configures the elapsed execution time to be dumped to SharpPad.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="title">The title to associate with the elapsed time in the SharpPad pane.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <remarks>
        /// The dump to SharpPad will be executed asynchronously. There is no guarantee that this dump will have
        /// completed before further configured actions on <paramref name="setup"/> begin executing.
        /// </remarks>
        /// <example>
        /// <code>
        /// setup.DumpToSharpPad("Elapsed time for algorithm");
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> DumpToSharpPad(this Setup setup, string title) =>
            setup.Do(elapsed => elapsed.Dump(title));

        /// <summary>
        /// Configures the elapsed execution time to be dumped to SharpPad.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="port">The port the SharpPad server is running on.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <remarks>
        /// The dump to SharpPad will be executed asynchronously. There is no guarantee that this dump will have
        /// completed before further configured actions on <paramref name="setup"/> begin executing.
        /// </remarks>
        /// <example>
        /// <code>
        /// setup.DumpToSharpPad(45960);
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> DumpToSharpPad(this Setup setup, int port) =>
            setup.Do(elapsed => elapsed.DumpOnPort(port));

        /// <summary>
        /// Configures the elapsed execution time to be dumped to SharpPad.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="title">The title to associate with the elapsed time in the SharpPad pane.</param>
        /// <param name="port">The port the SharpPad server is running on.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <remarks>
        /// The dump to SharpPad will be executed asynchronously. There is no guarantee that this dump will have
        /// completed before further configured actions on <paramref name="setup"/> begin executing.
        /// </remarks>
        /// <example>
        /// <code>
        /// setup.DumpToSharpPad("Elapsed time for algorithm", 45960);
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> DumpToSharpPad(this Setup setup, string title, int port) =>
            setup.Do(elapsed => elapsed.DumpOnPort(port, title));

        private static async Task DumpOnPort(this TimeSpan elapsed, int port, string title = null)
        {
            var originalPort = Output.Port;
            Output.Port = port;
            await elapsed.Dump(title);
            Output.Port = originalPort;
        }
    }
}