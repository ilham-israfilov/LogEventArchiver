using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.IO;
using System.Text;

namespace LogEventArchiver
{
    public class LogFileReader : ILogFileReader
    {
        private string _logFile;
        private long _alertThreshold;

        public LogFileReader(string logFile, long alertThreshold)
        {
            _logFile = logFile;
            _alertThreshold = alertThreshold;
        }

        public async void ReadAndParseAsync(BlockingCollection<ServerEvent> events)
        {
            using (var file = File.OpenRead(_logFile))
            using (var reader = new StreamReader(file, Encoding.ASCII))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var parserObj = JObject.Parse(line);
                    var srvEvent = ServerEventFactory.GetServerEvent(parserObj);
                    events.Add(srvEvent);
                }
            }
        }
    }
}