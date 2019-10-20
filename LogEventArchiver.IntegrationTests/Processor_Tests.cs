using LogEventArchiver.Importer;
using LogEventArchiver.Models;
using LogEventArchiver.Reader;
using System;
using Xunit;

namespace LogEventArchiver.IntegrationTests
{
    public class Processor_Tests
    {
        [Fact]
        public void Run_Succeeds()
        {
            string logFilePath = "TestFiles\\LogFileReader_Test_Data.txt";
            var reader = new LogFileReader(logFilePath, 4L);

            string dbFilePath = @$"{nameof(LogEventArchiver)}_{Guid.NewGuid().ToString("N")}.db";
            string collectionName = "ServerEvents";
            LiteDbQueryManager.ClearCollection<ServerEvent>(dbFilePath, collectionName, s => true);

            int maxReaderThreads = 16;
            var importer = new LogEventLiteDbImporter(maxReaderThreads, dbFilePath);

            var sut = new Processor(reader, importer);
            sut.Run();

            var srvEvtDocs = LiteDbQueryManager.GetDocuments<ServerEvent>(dbFilePath, collectionName, s => true);

            Assert.NotEmpty(srvEvtDocs);
        }
    }
}