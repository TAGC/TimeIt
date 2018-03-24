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
            var length = values.Count();

            if (length <= 1)
            {
                return values;
            }

            var midpoint = length / 2;
            var left = Sort(values.Take(midpoint), comparer);
            var right = Sort(values.TakeLast(length - midpoint), comparer);

            return Merge(left, right, comparer);
        }

        private IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer)
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
