namespace Catalog.API.Models
{
    public class CategoryNew
    {
        public string CategoryName { get; set; }
        public string Subcategory { get; set; }
        public int DepartmentId { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        
    }
}
