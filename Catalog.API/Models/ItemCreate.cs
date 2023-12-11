namespace Catalog.API.Models
{
    public class ItemCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal JoiningFees { get; set; }
        public decimal AnnualFees { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsPhysical { get; set; }
        public string Image { get; set; }
        
    }
}
