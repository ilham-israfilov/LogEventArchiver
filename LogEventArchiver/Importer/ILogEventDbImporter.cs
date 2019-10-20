using LogEventArchiver.Models;

using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogEventArchiver.Importer
{
    public interface ILogEventDbImporter
    {
        Task Import(BlockingCollection<ServerEvent> events);
    }
}