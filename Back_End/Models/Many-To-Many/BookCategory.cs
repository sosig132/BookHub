using Back_End.Models.Books;
using Back_End.Models.Categories;

namespace Back_End.Models.Many_To_Many
{
    public class BookCategory
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
