using System;
using System.Collections.Generic;
using System.Linq;

namespace Librarian.Sorting
{
    public class QuickSort : ISortStrategy
    {
        public static QuickSort Instance { get; } = new QuickSort();

        public IEnumerable<T> Sort<T>(IEnumerable<T> values, IComparer<T> comparer)
        {
            if (values.Count() <= 1)
            {
                return values;
            }

            var (left, pivot, right) = Partition(values, comparer);
            var sortedLeft = Sort(left, comparer);
            var sortedRight = Sort(right, comparer);

            return sortedLeft.Append(pivot).Concat(sortedRight);
        }

        private static (IEnumerable<T> left, T pivot, IEnumerable<T> right) Partition<T>(
            IEnumerable<T> values,
            IComparer<T> comparer)
        {
            var pivot = values.First();
            var left = new List<T>();
            var right = new List<T>();

            foreach (var element in values.Skip(1))
            {
                switch (Math.Sign(comparer.Compare(element, pivot)))
                {
                    // element <= pivot
                    case -1:
                    case 0:
                        left.Add(element);
                        continue;

                    // element > pivot
                    case 1:
                        right.Add(element);
                        continue;
                }
            }

            return (left, pivot, right);
        }
    }
}
