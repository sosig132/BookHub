using Back_End.Models.Books;

namespace Back_End.Models.BookDetails
{
    public class BookDetails : BaseEntity.BaseEntity
    {
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public Book? Book { get; set; }
        public Guid BookId { get; set; }

        
    }
}
