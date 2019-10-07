using System;
using System.Linq;

namespace LogEventArchiver
{
    internal class EntryPoint
    {
        private static void Main(string[] args)
        {
            var parameters = args.Select(a => a.Split('=')).ToDictionary(a => a[0].TrimStart('-'), b => b[1]);

            var reader = new LogFileReader(parameters["logFile"], long.Parse(parameters["alertThreshold"]));
            var importer = new LogEventDbImporter();

            var processor = new Processor(reader, importer);

            processor.Run();

            Console.ReadLine();
        }
    }
}