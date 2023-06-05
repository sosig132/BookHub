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
    }
}
