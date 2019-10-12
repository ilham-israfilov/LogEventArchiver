namespace LogEventArchiver.Models
{
    public class ServerEvent
    {
        public string Id { get; set; }
        public long Duration { get; set; }
        public bool Alert { get; set; }
    }
}