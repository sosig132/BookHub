using Back_End.Data;
using Back_End.Models.Categories;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.CategoryRepository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context) : base(context)
        {
            _context = context;
        }

        public Category GetCategoryMappedByName(string name)
        {
            return _table.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }

        public Category GetCategoryMappedById(Guid id)
        {
            return _table.FirstOrDefault(x => x.Id == id);
        }

        public List<Category> GetCategoriesMappedByBookId(Guid id)
        {
            return _context.BookCategories.Where(x => x.BookId == id).Select(x => x.Category).ToList();
        }
    }
}
