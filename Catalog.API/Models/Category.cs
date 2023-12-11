namespace Catalog.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName {  get; set; }
        public string Subcategory { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
    }
}
