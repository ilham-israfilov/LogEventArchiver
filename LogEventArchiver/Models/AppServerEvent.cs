namespace LogEventArchiver.Models
{
    public class AppServerEvent : ServerEvent
    {
        public string Type { get; set; }
        public string Host { get; set; }
    }
}