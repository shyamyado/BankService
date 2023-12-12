namespace ApplicationProcess.API.Model
{
    public class ApplicationStatus
    {
        public int ApplicationId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
