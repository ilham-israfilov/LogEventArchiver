using LogEventArchiver.Models;
using LogEventArchiver.Reader;

using System.Collections.Concurrent;
using System.Threading.Tasks;

using Xunit;

namespace LogEventArchiver.IntegrationTests
{
    public class LogFileReader_Tests
    {
        [Fact]
        public void ReadAllEvents_Fills_Collection_With_Event_Objects()
        {
            string logFilePath = "TestFiles\\LogFileReader_Test_Data.txt";
            var events = new BlockingCollection<ServerEvent>();

            var sut = new LogFileReader(logFilePath, 4L);
            var returnedTask = sut.ReadAllEvents(events);
            Task.WaitAll(returnedTask);

            Assert.NotEmpty(events);
        }
    }
}