using Catalog.API.Models;

namespace Catalog.API.Infrastructure.IntegrationEvents.Events
{
    public class ItemPriceChangeIntegrationEvent
    {
        public int Id { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public ItemPriceChangeIntegrationEvent(ItemPriceChange item)
        {
            Id = item.Id;
            OldPrice = item.OldPrice;
            NewPrice = item.NewPrice;
        }
    }
}
