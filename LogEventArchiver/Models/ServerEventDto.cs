using Newtonsoft.Json;

namespace LogEventArchiver.Models
{
    public class ServerEventDto
    {
        [JsonProperty(PropertyName = "id", Order = 1, Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "state", Order = 2, Required = Required.Always)]
        public string State { get; set; }

        [JsonProperty(PropertyName = "type", Order = 3, Required = Required.Default)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "host", Order = 4, Required = Required.Default)]
        public string Host { get; set; }

        [JsonProperty(PropertyName = "timestamp", Order = 5, Required = Required.Always)]
        public long Timestamp { get; set; }
    }
}