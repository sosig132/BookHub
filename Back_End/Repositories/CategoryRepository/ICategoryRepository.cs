using Back_End.Models.Categories;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.CategoryRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category GetCategoryMappedByName(string name);

    }
}
