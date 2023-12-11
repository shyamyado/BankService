using Catalog.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Catalog.API.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDBContext _dBContext;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(CatalogDBContext dBContext, ILogger<CategoryRepository> logger)
        {
            _dBContext = dBContext ?? throw new ArgumentNullException(nameof(DbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> AddCategory(Category category)
        {

            try
            {
                await _dBContext.Categories.AddAsync(category);
                var res = await _dBContext.SaveChangesAsync();
                return res;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
            {
                throw new Exception("Category with the same name already exists.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add category.", ex);
            }
        }

        public async Task<int> AddDepartment(DepartmentNew department)
        {

            try
            {
                Department dept = new Department
                {
                    Name = department.Name,
                    Description = department.Description
                };
                _dBContext.Departments.Add(dept);
                return await _dBContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new Exception("Department with the same name already exists.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add department.", ex);
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var res = await _dBContext.Categories.ToListAsync();
                _logger.LogInformation("All Categories being listed.");
                return res;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error occurred while retrieving categories.");
                if (sqlEx.Number == 53)
                {
                    throw new Exception("Error connecting to the database. Please check the database connection.", ex);
                }
                throw new Exception("Failed to retrieve categories.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving categories.");
                throw new Exception("Failed to retrieve categories.", ex);
            }
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            try
            {
                var res = await _dBContext.Departments.ToListAsync();
                return res;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database error occurred while retrieving departments.");
                throw new Exception("Failed to retrieve departments. Please check the log for details.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving departments.");
                throw new Exception("Failed to retrieve departments. Please check the log for details.", ex);
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var res = await _dBContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (res == null)
                {
                    _logger.LogError($"Category with ID {id} not found.");
                    throw new Exception($"Category with ID {id} not found.");
                }
                return res;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error occurred while retrieving category with ID {id}.");
                throw new Exception("Error retrieving category from the database.", ex);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve category with ID {id}.");
                throw new Exception($"Failed to retrieve category with ID {id}. Please check the log for details.", ex);
            }
            
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            try
            {
                var res = await _dBContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
                return res;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error occurred while retrieving department with ID {id}.");
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve department with ID {id}.");
                throw new Exception($"Failed to retrieve department with ID {id}.", ex );
            }
        }

        public async Task<int?> UpdateCategory(Category category)
        {
            try
            {
                var itemToUpdate = await _dBContext.Categories.FirstOrDefaultAsync(x => x.Id ==category.Id);

                if (itemToUpdate != null)
                {
                    _dBContext.Entry(itemToUpdate).CurrentValues.SetValues(category);
                    return await _dBContext.SaveChangesAsync();
                }
                return null;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error occurred while retrieving category with ID {category.Id}.");
                return null;
            }
            catch (Exception ex)
            {
                // Log and handle the exception
                _logger.LogError(ex, $"Error occurred while updating category with ID {category.Id}.");
                throw new Exception($"Failed to update category");
            }
        }

        public async Task<int?> UpdateDepartment(Department department)
        {
            try
            {
                var departmentToUpdate = await _dBContext.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);
                if(departmentToUpdate != null)
                {
                    _dBContext.Entry(departmentToUpdate).CurrentValues.SetValues(department);
                    return await _dBContext.SaveChangesAsync();
                }
                return null;
            }
            catch(DbException ex) when (ex.InnerException is SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Database error occurred while retrieving department with ID {department.Id}.");
                return null;
            }
            catch(Exception ex) {
                _logger.LogError(ex, $"Error occurred while updating department with ID {department.Id}.");
                throw new Exception($"Faied to update department");
            }
        }
    }
}
