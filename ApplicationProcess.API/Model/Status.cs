namespace ApplicationProcess.API.Model
{
    public class Status
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;} = DateTime.Now;

    }
}
