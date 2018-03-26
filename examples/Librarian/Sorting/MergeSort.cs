using System;
using System.Collections.Generic;
using System.Linq;

namespace Librarian.Sorting
{
    public class MergeSort : ISortStrategy
    {
        public static MergeSort Instance { get; } = new MergeSort();

        public IEnumerable<T> Sort<T>(IEnumerable<T> values, IComparer<T> comparer)
        {
            var valuesArray = values as T[] ?? values.ToArray();
            var length = valuesArray.Length;

            if (length <= 1)
            {
                return valuesArray;
            }

            var midpoint = length / 2;
            var left = Sort(valuesArray.Take(midpoint), comparer);
            var right = Sort(valuesArray.TakeLast(length - midpoint), comparer);

            return Merge(left, right, comparer);
        }

        private static IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer)
        {
            var leftQueue = new Queue<T>(left);
            var rightQueue = new Queue<T>(right);

            while (leftQueue.Any() && rightQueue.Any())
            {
                switch (Math.Sign(comparer.Compare(leftQueue.Peek(), rightQueue.Peek())))
                {
                    // left <= right
                    case -1:
                    case 0:
                        yield return leftQueue.Dequeue();
                        continue;

                    // left > right
                    case 1:
                        yield return rightQueue.Dequeue();
                        continue;
                }
            }

            foreach (var remaining in leftQueue)
            {
                yield return remaining;
            }

            foreach (var remaining in rightQueue)
            {
                yield return remaining;
            }
        }
    }
}