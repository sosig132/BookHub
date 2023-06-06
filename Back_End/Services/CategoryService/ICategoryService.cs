using Back_End.Models.Categories;

namespace Back_End.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Category AddUser(Category category);
        bool SaveChanges();
        Category GetByName(string name);
        List<Category> GetByBookId(Guid id);
        Category GetById(Guid id);
    }
}
