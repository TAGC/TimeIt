using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Librarian.DataAccess
{
    public class GutenbergBookRepository : IBookRepository
    {
        private static readonly Regex BookRegex = new Regex(
            @"(?<Title>.*), by (?<Author>(\s?[\w.])*)",
            RegexOptions.Singleline);

        private static readonly Regex EtExtRegex = new Regex(@"\s{2,}\d{5}");

        private readonly string _filePath;

        public GutenbergBookRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Book> GetBooks()
        {
            var text = File.ReadAllText(_filePath);

            foreach (var chunk in text.Split("\n\n"))
            {
                if (TryParseBook(chunk, out var book))
                {
                    // ReSharper disable once PossibleInvalidOperationException
                    yield return book.Value;
                }
            }
        }

        private static bool TryParseBook(string input, out Book? book)
        {
            // Verify an ET EXT No. exists in the chunk;
            if (!EtExtRegex.IsMatch(input))
            {
                book = null;
                return false;
            }

            // Try parsing the chunk with the ET EXT No. filtered out.
            var filtered = EtExtRegex.Replace(input, string.Empty);
            var bookMatch = BookRegex.Match(filtered);

            if (!bookMatch.Success)
            {
                book = null;
                return false;
            }

            var title = bookMatch.Groups["Title"].Value.Replace("\n", string.Empty);
            var author = bookMatch.Groups["Author"].Value;
            book = new Book(title, author);
            return true;
        }
    }
}