using LogEventArchiver.Importer;
using LogEventArchiver.Models;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace LogEventArchiver.IntegrationTests
{
    public class LogEventDbImporter_Tests
    {
        [Fact]
        public void Import_Succeeds()
        {
            string dbFilePath = @$"{nameof(LogEventArchiver)}_{Guid.NewGuid().ToString("N")}.db";
            string collectionName = "ServerEvents";
            LiteDbQueryManager.ClearCollection<ServerEvent>(dbFilePath, collectionName, s => true);

            int maxReaderThreads = 16;
            var events = this.getGeneratedServerEventsCollection();
            int eventCount = events.Count;

            var sut = new LogEventLiteDbImporter(maxReaderThreads, dbFilePath);
            var returnedTask = sut.Import(events);
            Task.WaitAll(returnedTask);

            var srvEvtDocs = LiteDbQueryManager.GetDocuments<ServerEvent>(dbFilePath, collectionName, s => true);

            Assert.Equal(eventCount, srvEvtDocs.Count());
        }

        private BlockingCollection<ServerEvent> getGeneratedServerEventsCollection()
        {
            var result = new BlockingCollection<ServerEvent>();

            var randomizer = new Random();

            for (int i = 0; i < 10000; i++)
            {
                string srvEvtId = Guid.NewGuid().ToString();
                long srvEvtDuration = randomizer.Next(1001);
                bool srvEvtAlert = srvEvtDuration % 2 == 0;

                var srvEvt = new ServerEvent { Id = srvEvtId, Duration = srvEvtDuration, Alert = srvEvtAlert };

                string appSrvEvtId = Guid.NewGuid().ToString();
                long appSrvEvtDuration = randomizer.Next(1001);
                bool appSrvEvtAlert = srvEvtDuration % 2 != 0;
                string appSrvEvtType = "TEST_APP";
                string appSrvEvtHost = "localhost";

                var appSrvEvt = new AppServerEvent { Id = appSrvEvtId, Duration = appSrvEvtDuration, Alert = appSrvEvtAlert, Type = appSrvEvtType, Host = appSrvEvtHost };

                if (randomizer.Next(1001) % 2 == 0)
                {
                    result.Add(srvEvt);
                    result.Add(appSrvEvt);
                }
                else
                {
                    result.Add(appSrvEvt);
                    result.Add(srvEvt);
                }
            }

            result.CompleteAdding();

            return result;
        }
    }
}