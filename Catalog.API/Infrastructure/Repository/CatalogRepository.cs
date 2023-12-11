using Catalog.API.Models;
using Catalog.API.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Infrastructure.Repository
{

    public class CatalogRepository : ICatalogRepository
    {

        public readonly CatalogDBContext _dbContext;
        public readonly ILogger<CatalogRepository> _logger;

        public CatalogRepository(CatalogDBContext catalogDBContext, ILogger<CatalogRepository> logger)
        {
            _dbContext = catalogDBContext ?? throw new ArgumentNullException(nameof(catalogDBContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<ItemViewModel>> GetAllItems(int page, int pageSize)
        {
            int itemsToSkip = (page - 1) * pageSize;

            try
            {
                var res = await _dbContext.Items
                                .OrderBy(item => item.Id)
                                .Skip(itemsToSkip)
                                .Take(pageSize)
                                .ToListAsync();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving items.");
                throw new Exception("Error retiveing items.");
            }
        }

        public async Task<ItemViewModel> AddItem(ItemCreate item)
        {
            try
            {
                ItemViewModel new_item = new ItemViewModel
                {
                    Name = item.Name,
                    Description = item.Description,
                    ShortDescription = item.ShortDescription,
                    JoiningFees = item.JoiningFees,
                    AnnualFees = item.AnnualFees,
                    CategoryId = item.CategoryId,
                    IsActive = item.IsActive,
                    IsPhysical = item.IsPhysical,
                    Image = item.Image,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _dbContext.Items.Add(new_item);
                await _dbContext.SaveChangesAsync();
                return new_item;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    _logger.LogError(sqlException, "Foreign key constraint violation. CategoryId does not exist.");
                    throw new Exception("CategoryId does not exist");
                }
                else
                {
                    _logger.LogError(ex, $"DbUpdateException: {ex.Message}");
                    throw new Exception("Failed to add item due to a database error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding an item.");
                throw new Exception("Failed to add item due to an unexpected error.");
            }
        }

        public async Task<ItemViewModel> GetItemById(int id)
        {
            try
            {
                var res = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving item with ID {id}.");
                throw new Exception($"Error retrieving item with ID {id}.", ex);
            }
        }
        public async Task<ItemViewModel> UpdateItem(ItemUpdate item)
        {
            try
            {
                var itemToUpdate = _dbContext.Items.FirstOrDefault(x => x.Id == item.Id);
                if (itemToUpdate != null)
                {
                    _dbContext.Entry(itemToUpdate).CurrentValues.SetValues(item);
                    await _dbContext.SaveChangesAsync();
                }
                var updatedItem = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == item.Id);
                return updatedItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item with ID {item.Id}.");
                throw new Exception($"Error updating item with ID {item.Id}.", ex);
            }
        }

        public void UpDateItemIsActive(int ItemId, bool status)
        {
            try
            {
                var item = _dbContext.Items.FirstOrDefault(x => x.Id == ItemId);
                if (item != null)
                {
                    item.IsActive = status;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for item with ID {ItemId}");
                throw new Exception("Error updating status for item with ID", ex);
            }
        }


        public void UpdatePrice(Price price)
        {
            throw new NotImplementedException();
        }




    }
}
