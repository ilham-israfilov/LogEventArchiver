using LogEventArchiver.Models;

using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogEventArchiver
{
    public interface ILogEventDbImporter
    {
        Task Import(BlockingCollection<ServerEvent> events);
    }
}