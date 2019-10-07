namespace LogEventArchiver
{
    public class AppServerEvent : ServerEvent
    {
        public string Type { get; set; }
        public string Host { get; set; }
    }
}