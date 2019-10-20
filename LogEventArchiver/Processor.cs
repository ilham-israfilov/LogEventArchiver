using LogEventArchiver.Importer;
using LogEventArchiver.Models;
using LogEventArchiver.Reader;

using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogEventArchiver
{
    public class Processor
    {
        private ILogFileReader _reader;
        private ILogEventDbImporter _importer;

        public Processor(ILogFileReader reader, ILogEventDbImporter importer)
        {
            _reader = reader;
            _importer = importer;
        }

        public void Run()
        {
            using (var events = new BlockingCollection<ServerEvent>())
            {
                var readerTask = _reader.ReadAllEvents(events);
                var importerTask = _importer.Import(events);

                Task.WaitAll(readerTask, importerTask);
            }
        }
    }
}