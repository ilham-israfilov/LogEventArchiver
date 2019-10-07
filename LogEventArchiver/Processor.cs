using System.Collections.Concurrent;

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
            BlockingCollection<ServerEvent> events = new BlockingCollection<ServerEvent>();

            _reader.ReadAndParseAsync(events);
            _importer.Import(events);
        }
    }
}