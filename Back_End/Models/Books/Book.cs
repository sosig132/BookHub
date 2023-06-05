using Back_End.Models.Many_To_Many;
using Back_End.Models.Reviews;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Back_End.Models.Books
{
    public class Book : BaseEntity.BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; } // International Standard Book Number        
        public string Image { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        [JsonIgnore]
        public BookDetails.BookDetails? BookDetails { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }

        
    }
}
