using Back_End.Models.Categories;
using Back_End.Repositories.CategoryRepository;

namespace Back_End.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public Category AddUser(Category category)
        {
            _categoryRepository.Create(category);
            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAll();
        }

        public bool SaveChanges()
        {
            return _categoryRepository.Save();
        }

        public Category GetByName(string name)
        {
            return _categoryRepository.GetCategoryMappedByName(name);
        }
    }

}
