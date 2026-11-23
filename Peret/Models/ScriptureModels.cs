using System.Text.Json.Serialization;

namespace Peret.Models
{
    public class ScriptureRoot
    {
        [JsonPropertyName("books")]
        public List<BookData> Books { get; set; } = new();
    }

    public class BookData
    {
        [JsonPropertyName("book")]
        public string Book { get; set; } = string.Empty;

        [JsonPropertyName("chapters")]
        public List<ChapterData> Chapters { get; set; } = new();
    }

    public class ChapterData
    {
        [JsonPropertyName("chapter")]
        public int Chapter { get; set; }

        [JsonPropertyName("verses")]
        public List<VerseData> Verses { get; set; } = new();
    }

    public class VerseData
    {
        [JsonPropertyName("verse")]
        public int Verse { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}