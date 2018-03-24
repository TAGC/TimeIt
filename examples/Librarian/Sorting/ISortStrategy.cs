using System;
using System.Collections.Generic;

namespace Librarian.Sorting
{
    public interface ISortStrategy
    {
        IEnumerable<T> Sort<T>(IEnumerable<T> values, IComparer<T> comparer);
    }
}
