using System;

namespace Arbetsprov.Models {
    public class SearchResult {
        public string Query {get;set;}
        public string Title { get; set; } = "No Title Found";
        public string Author { get; set; } = "No Author Found";
        public bool HasIBM { get; set; } = false;
        public string Source { get; set; }

        public override string ToString()
        {
            return "Title: " + this.Title + "\nAuthor: " + this.Author + "\nHas IBM: " + this.HasIBM;
        }
    }
}