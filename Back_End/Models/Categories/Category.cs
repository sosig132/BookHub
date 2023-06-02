using Back_End.Models.Many_To_Many;

namespace Back_End.Models.Categories
{
    public class Category : BaseEntity.BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
