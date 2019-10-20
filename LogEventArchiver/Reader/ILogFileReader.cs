using LogEventArchiver.Models;

using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogEventArchiver.Reader
{
    public interface ILogFileReader
    {
        Task ReadAllEvents(BlockingCollection<ServerEvent> events);
    }
}