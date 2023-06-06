using Back_End.Models.Categories;
using Back_End.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService= categoryService;

        }
        // GET
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var users = await _categoryService.GetAllCategories();
            return Ok(users);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category is null");
            }

            if (category.Name == null)
                return BadRequest("Category name is null");

            category.Id = Guid.NewGuid();
            category.DateCreated = DateTime.Now;

            if (_categoryService.GetByName(category.Name) != null) {
                return BadRequest("Category already exists" );
            }
            _categoryService.AddUser(category);
            _categoryService.SaveChanges();
            return Ok(category);
        }
        //create get method for category by id
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }
    }
}
