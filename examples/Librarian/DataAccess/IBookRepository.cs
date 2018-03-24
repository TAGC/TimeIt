using System;
using System.Collections.Generic;

namespace Librarian.DataAccess
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
    }
}
