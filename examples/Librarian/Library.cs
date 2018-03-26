using System;
using System.Collections.Generic;
using System.Linq;
using Librarian.DataAccess;
using Librarian.Sorting;

namespace Librarian
{
    public class Library
    {
        private static readonly IComparer<Book> AuthorThenName = new BookComparer();

        private readonly List<Book> _books;
        private readonly ISortStrategy _bookSortingStrategy;

        private bool _sortedBooks;

        public Library(IBookRepository bookRepository, ISortStrategy bookSortingStrategy)
        {
            _books = bookRepository.GetBooks().ToList();
            _bookSortingStrategy = bookSortingStrategy;
            _sortedBooks = false;
        }

        public void PrintBooks()
        {
            if (!_sortedBooks)
            {
                SortBooks();
            }

            foreach (var collection in _books.GroupBy(it => it.Author))
            {
                var author = collection.Key;

                Console.WriteLine(author);
                Console.WriteLine("--------------------\n");

                foreach (var book in collection)
                {
                    Console.WriteLine("\t" + book.Title);
                }

                Console.WriteLine();
            }
        }

        public void SortBooks()
        {
            var sorted = _bookSortingStrategy.Sort(_books, AuthorThenName).ToList();

            _books.Clear();
            _books.AddRange(sorted);
            _sortedBooks = true;
        }

        private class BookComparer : IComparer<Book>
        {
            public int Compare(Book x, Book y)
            {
                var authorComparison = string.Compare(x.Author, y.Author, StringComparison.Ordinal);
                var nameComparison = string.Compare(x.Title, y.Title, StringComparison.Ordinal);

                return authorComparison != 0 ? authorComparison : nameComparison;
            }
        }
    }
}