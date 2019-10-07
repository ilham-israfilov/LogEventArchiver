using System.Collections.Concurrent;

namespace LogEventArchiver
{
    public interface ILogFileReader
    {
        void ReadAndParseAsync(BlockingCollection<ServerEvent> events);
    }
}