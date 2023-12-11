using Catalog.API.Models;

namespace Catalog.API.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal JoiningFees { get; set; }
        public decimal AnnualFees { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsPhysical { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
