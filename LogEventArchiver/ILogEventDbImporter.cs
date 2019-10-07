using System.Collections.Concurrent;

namespace LogEventArchiver
{
    public interface ILogEventDbImporter
    {
        void Import(BlockingCollection<ServerEvent> events);
    }
}