using Catalog.API.Models;

namespace Catalog.API.Infrastructure.Repository
{
    public interface ICategoryRepository
    {
        public Task<int> AddCategory(Category category);
        public Task<Category> GetCategoryById(int id);
        public Task<List<Category>> GetAllCategories();
        public Task<int?> UpdateCategory(Category category);

        public Task<int> AddDepartment(DepartmentNew department);
        public Task<Department> GetDepartmentById(int id);
        public Task<List<Department>> GetAllDepartments();
        public Task<int?> UpdateDepartment(Department department);
    }
}
