using Catalog.API.Models;
using Catalog.API.ViewModels;

namespace Catalog.API.Infrastructure.Repository
{
    public interface ICatalogRepository
    {
        public Task<ItemViewModel> AddItem(ItemCreate item);
        public Task<List<ItemViewModel>> GetAllItems(int page, int pageSize);
        public Task<ItemViewModel> GetItemById(int id);
        public Task<ItemViewModel> UpdateItem(ItemUpdate item);
        public void UpDateItemIsActive(int ItemId, bool status);
        public void UpdatePrice(Price price);
    }
}
