using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface ICategoryService
    {
        public Task<int> AddCategory(CategoryNew category);
        public Task<Category> GetCategoryById(int id);
        public Task<List<Category>> GetAllCategories();
        public Task<string> UpdateCategory(Category category);

        public Task<int> AddDepartment(DepartmentNew department);
        public Task<Department> GetDepartmentById(int id);
        public Task<List<Department>> GetAllDepartments();
        public Task<string> UpdateDepartment(Department department);
    }
}
