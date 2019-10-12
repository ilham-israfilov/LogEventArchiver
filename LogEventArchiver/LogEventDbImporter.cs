using LogEventArchiver.Models;

using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogEventArchiver
{
    public class LogEventDbImporter : ILogEventDbImporter
    {
        public Task Import(BlockingCollection<ServerEvent> events)
        {
            var runnerTask = new Task(() =>
            {

            });

            runnerTask.Start();

            return runnerTask;
        }
    }
}