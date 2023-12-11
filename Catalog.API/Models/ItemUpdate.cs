namespace Catalog.API.Models
{
    public class ItemUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal JoiningFees { get; set; }
        public decimal AnnualFees { get; set; }
        public int CategoryID { get; set; }
        public bool IsActive { get; set; }
        public bool IsPhysical { get; set; }
        public string Image { get; set; }
    }
}
