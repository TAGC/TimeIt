using System;

namespace Librarian
{
    public readonly struct Book
    {
        public Book(string title, string author)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }

        public string Title { get; }

        public string Author { get; }

        public override string ToString() => $"{Title}, by {Author}";
    }
}
