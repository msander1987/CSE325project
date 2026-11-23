using System.Text.Json;
using Peret.Models;

namespace Peret.Services
{
    public class ScriptureService
    {
        private readonly ScriptureRoot _scriptures;

        public ScriptureService(IWebHostEnvironment env)
        {
            var filePath = Path.Combine(env.WebRootPath, "data", "book-of-mormon.json");
            
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                _scriptures = JsonSerializer.Deserialize<ScriptureRoot>(json) ?? new ScriptureRoot();
            }
            else
            {
                _scriptures = new ScriptureRoot();
            }
        }

        // returns de names of the books
        public List<string> GetBookNames()
        {
            return _scriptures.Books.Select(b => b.Book).ToList();
        }

        // returns the number of chapters for each book
        public List<int> GetChapterNumbers(string bookName)
        {
            var book = _scriptures.Books.FirstOrDefault(b => b.Book == bookName);
            return book?.Chapters.Select(c => c.Chapter).ToList() ?? new List<int>();
        }

        // return a complet chapter with verses
        public ChapterData? GetChapter(string bookName, int chapterNumber)
        {
            var book = _scriptures.Books.FirstOrDefault(b => b.Book == bookName);
            return book?.Chapters.FirstOrDefault(c => c.Chapter == chapterNumber);
        }

        // returns a random chapter
        public (string BookName, ChapterData Chapter)? GetRandomChapter()
        {
            if (_scriptures.Books.Count == 0) return null;

            var random = new Random();
            var randomBook = _scriptures.Books[random.Next(_scriptures.Books.Count)];
            if (randomBook.Chapters.Count == 0) return null;

            var randomChapter = randomBook.Chapters[random.Next(randomBook.Chapters.Count)];
            return (randomBook.Book, randomChapter);
        }
    }
}