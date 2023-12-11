using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Catalog.API.Infrastructure.Repository;
using Catalog.API.Models;

namespace Catalog.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> AddCategory(CategoryNew category)
        {
            try
            {
                Category category1 = new Category
                {
                    CategoryName = category.CategoryName,
                    Subcategory = category.Subcategory,
                    DepartmentId = category.DepartmentId,
                    Type = category.Type,
                    IsActive = category.IsActive,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                var result = await _repository.AddCategory(category1);
                _logger.LogInformation($"Category added successfully. Category ID: {result}");
                return result;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
            {
                // Log and handle duplicate key violation (unique constraint)
                _logger.LogError(sqlEx, "Category with the same name already exists.");
                throw new Exception("Category with the same name already exists.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Log and handle other database update errors
                _logger.LogError(ex, "Failed to add category to the database.");
                throw new Exception("Failed to add category to the database.", ex);
            }
            catch (Exception ex)
            {
                // Log and handle other unexpected errors
                _logger.LogError(ex, "An unexpected error occurred while adding a category.");
                throw new Exception("Failed to add category due to an unexpected error.", ex);
            }
        }

        public async Task<int> AddDepartment(DepartmentNew department)
        {
            var res = await _repository.AddDepartment(department);
            return res;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var res = await _repository.GetAllCategories();
            return res;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            var res = await _repository.GetAllDepartments();
            return res;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var res = await _repository.GetCategoryById(id);
            return res;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var res = await _repository.GetDepartmentById(id);
            return res;
        }

        public Task<string> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateDepartment(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
