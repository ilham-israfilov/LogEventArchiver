using LogEventArchiver.Importer;
using LogEventArchiver.Reader;
using System;
using System.Linq;

namespace LogEventArchiver
{
    internal class EntryPoint
    {
        private static void Main(string[] args)
        {
            var parameters = args.Select(a => a.Split('=')).ToDictionary(a => a[0].TrimStart('-'), b => b[1]);
            string connectionString = $"{nameof(LogEventArchiver)}.db";

            var reader = new LogFileReader(parameters["logFile"], long.Parse(parameters["alertThreshold"]));
            var importer = new LogEventLiteDbImporter(int.Parse(parameters["importerMaxReaderThreads"]), connectionString);

            var processor = new Processor(reader, importer);

            processor.Run();

            Console.ReadLine();
        }
    }
}