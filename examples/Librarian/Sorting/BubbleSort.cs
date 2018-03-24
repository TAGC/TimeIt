using System;
using System.Collections.Generic;
using System.Linq;

namespace Librarian.Sorting
{
    public class BubbleSort : ISortStrategy
    {
        public static BubbleSort Instance { get; } = new BubbleSort();

        public IEnumerable<T> Sort<T>(IEnumerable<T> values, IComparer<T> comparer)
        {
            var valueArray = values.ToArray();
            var swappedWithinPass = false;

            do
            {
                swappedWithinPass = false;
                
                for (int i=0; i < valueArray.Length - 1; i++)
                {
                    if (comparer.Compare(valueArray[i], valueArray[i + 1]) > 0)
                    {
                        var temp = valueArray[i];
                        valueArray[i] = valueArray[i + 1];
                        valueArray[i + 1] = temp;
                        swappedWithinPass = true;
                    }
                }
            }
            while (swappedWithinPass);

            return valueArray;
        }
    }
}
