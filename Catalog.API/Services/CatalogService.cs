using Catalog.API.Infrastructure.Repository;
using Catalog.API.Models;
using Catalog.API.ViewModels;
using NLog;
using NLog.Targets;
using RabbitMQEventBus;
using System.Data.SqlTypes;

namespace Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly ILogger<CatalogService> _logger;
        private readonly IRabbitMQPersistantConnection _rabbitMQPersistantConnection;

        public CatalogService(ICatalogRepository catalogRepository, IRabbitMQPersistantConnection rabbitMQPersistantConnection, ILogger<CatalogService> logger)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(CatalogRepository));
            _rabbitMQPersistantConnection = rabbitMQPersistantConnection ?? throw new ArgumentNullException(nameof(rabbitMQPersistantConnection)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ItemViewModel> AddItem(ItemCreate item)
        {
            try
            {
                var result = await _catalogRepository.AddItem(item);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an item.");
                throw new Exception("Exception :", ex);
            }
        }

        public async Task<List<ItemViewModel>> GetAllItems(int page, int pageSize)
        {
            try
            {

                var result = await _catalogRepository.GetAllItems(page, pageSize);
                _logger.LogInformation($"{DateTime.Now} : Retrieved {result.Count} items");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting all items: {ex.Message}");
                throw new Exception("Failed to retrieve items due to an unexpected error.", ex);
            }
        }
        public async Task<ItemViewModel> GetItemById(int id)
        {
            try
            {
                var res = await _catalogRepository.GetItemById(id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting item with ID {id}.");
                throw new Exception($"Error getting item with ID {id}.", ex);
            }
        }

        public async Task<ItemViewModel> UpdateItem(ItemUpdate item)
        {
            try
            {
                var res = await _catalogRepository.UpdateItem(item);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating item with ID {item.Id}.");
                throw new Exception($"Failed to update item with ID {item.Id} due to an unexpected error.", ex);
            }
        }

    }
}
