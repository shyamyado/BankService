namespace ApplicationProcess.API.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime? DOB { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set;} = DateTime.Now;
    }
}
