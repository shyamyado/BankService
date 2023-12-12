namespace ApplicationProcess.API.Model
{
    public class Application
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}= DateTime.Now;
    }
}
