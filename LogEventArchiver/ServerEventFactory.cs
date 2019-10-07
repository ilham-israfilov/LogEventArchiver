using Newtonsoft.Json.Linq;

namespace LogEventArchiver
{
    public static class ServerEventFactory
    {
        public static ServerEvent GetServerEvent(JObject rawObj)
        {
            return new ServerEvent();
        }
    }
}