using Microsoft.AspNetCore.Mvc;
using Catalog.API.Models;
using Catalog.API.Services;

namespace Catalog.API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        //public List<Task<ActionResult<Category>>> GetAllCategories()
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var res = await _categoryService.GetAllCategories();
            return Ok(res);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById([FromRoute] int id)
        {
            if (id == null|| id == 0)
            {
                return BadRequest("Category Id is Not valid");
            }
            var res = await _categoryService.GetCategoryById(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);

        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryNew category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Invalid category data.");
                }

                var categoryId = await _categoryService.AddCategory(category);

                if (categoryId > 0)
                {
                    _logger.LogInformation("Category Added.");
                    return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, categoryId);
                }
                else
                {
                    return StatusCode(500, "Failed to add category due to an unexpected error.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a category.");
                return StatusCode(500, "Failed to add category due to an unexpected error.");
            }
        }

        [HttpPut]
        public string UpdateCategory(Category category)
        {
            return "UPdate Category";
        }

        [HttpPost]
        [Route("Department")]
        public async Task<ActionResult<string>> AddDepartment(DepartmentNew department)
        {
            if(department == null)
            {
                return BadRequest("Department infoarmaiton is required.");
            }
            var res = await _categoryService.AddDepartment(department);
            return Ok(res);
        }

        [HttpGet]
        [Route("Department")]
        public async Task<ActionResult<List<Department>>> GetAllDepartment()
        {
            var res = await _categoryService.GetAllDepartments();
            return Ok(res);
        }

        [HttpGet]
        [Route("Department/{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById ([FromRoute]int id)
        {
            if(id  == 0 || id == null)
            {
                return BadRequest("department Id is not valid");
            }
            var res = await _categoryService.GetDepartmentById(id);
            return Ok(res);
        }
    }
}
