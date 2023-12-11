namespace Catalog.API.Models
{
    public class ItemPriceChange
    {
        public int Id { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}
