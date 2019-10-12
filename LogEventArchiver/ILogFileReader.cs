using LogEventArchiver.Models;

using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogEventArchiver
{
    public interface ILogFileReader
    {
        Task ReadAllEvents(BlockingCollection<ServerEvent> events);
    }
}