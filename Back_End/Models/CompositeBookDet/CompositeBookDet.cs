using Back_End.Models.Books;
using Back_End.Models.BookDetails;
using Back_End.Models.Many_To_Many;
using Back_End.Models.Reviews;

namespace Back_End.Models.CompositeBookDet
{
    public class CompositeBookDet : BaseEntity.BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; } // International Standard Book Number        
        public string Image { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public Guid BookId { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public Book? Book { get; set; }
        public BookDetails.BookDetails? BookDetails { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
