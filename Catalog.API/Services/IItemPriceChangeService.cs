using Catalog.API.Models;

namespace Catalog.API.Services
{
    public interface IItemPriceChangeService
    {
        public Task<string> PriceChange(ItemPriceChange item);
    }
}
