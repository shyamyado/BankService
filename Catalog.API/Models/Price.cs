using Catalog.API.ViewModels;

namespace Catalog.API.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public ItemViewModel Item { get; set; }
        public decimal Amount { get; set; }
        public decimal WithdrawlLimit { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
