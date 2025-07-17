using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ReadingList.Models
{
    // DEMO 0: DebuggerDisplay
    //[DebuggerDisplay("{Title}")]
    public class Book
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("pages")]
        public string Pages { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("image")]
        public string Cover { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        public int TimesRead { get; set; }

        public DateTime LastReadDate { get; set; }

        public string FinishedBookString()
        {
            // DEMO 3: ReturnValue, Step Into Specific, Format Spec - Add ToShortDateString() to LastReadDate; 
            // Remove ToShortDateString() to include the time book was finished
            return "last finished " + this.Title + " on " + LastReadDate;
        }
    }
}
