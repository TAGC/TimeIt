using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Librarian.DataAccess;
using Librarian.Sorting;
using Microsoft.Extensions.Logging;
using TimeItCore;

namespace Librarian
{
    public static class Program
    {
        private static IEnumerable<ISortStrategy> SortingStrategies
        {
            get
            {
                yield return BubbleSort.Instance;
                yield return MergeSort.Instance;
                yield return QuickSort.Instance;
            }
        }

        public static async Task Main()
        {
            var bookRepository = new GutenbergBookRepository("Resources/GUTINDEX.2018");
            var logger = new LoggerFactory().AddConsole(LogLevel.Trace).CreateLogger(typeof(Program));

            foreach (var sortingStrategy in SortingStrategies)
            {
                var library = new Library(bookRepository, sortingStrategy);

                logger.LogInformation("Sorting books using {SortingStrategy}", sortingStrategy);

                using (TimeIt.Then.Log(logger, LogLevel.Information, "Sorted books in {Elapsed}"))
                {
                    library.SortBooks();
                }

                // Prevents console output being displayed out of order.
                await Task.Delay(10);

                if (QueryToPrintBooks())
                {
                    library.PrintBooks();
                }

                Console.WriteLine("Press any key to continue...");
                Console.Read();
            }
        }

        private static bool QueryToPrintBooks()
        {
            Console.WriteLine("Print books? (y/n)");

            while (true)
            {
                switch (Console.ReadLine()?.ToLowerInvariant())
                {
                    case "y": return true;
                    case "n": return false;
                }
            }
        }
    }
}