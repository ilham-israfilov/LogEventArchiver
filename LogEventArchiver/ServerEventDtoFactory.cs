using LogEventArchiver.Models;

using Newtonsoft.Json;

namespace LogEventArchiver
{
    public static class ServerEventDtoFactory
    {
        public static ServerEventDto GetServerEventDto(string jsonString)
        {
            var result = JsonConvert.DeserializeObject<ServerEventDto>(jsonString);

            return result;
        }
    }
}