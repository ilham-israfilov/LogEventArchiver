using LiteDB;

using LogEventArchiver.Models;

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogEventArchiver.Importer
{
    public class LogEventLiteDbImporter : ILogEventDbImporter
    {
        private readonly int _maxReaderThreads;
        private readonly string _connectionString;

        public LogEventLiteDbImporter(int maxReaderThreads, string connectionString)
        {
            if (maxReaderThreads <= 0) throw new ArgumentOutOfRangeException(nameof(maxReaderThreads), "Positive number expected.");
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("A valid connection string expected.", nameof(connectionString));

            _maxReaderThreads = maxReaderThreads;
            _connectionString = connectionString;
        }

        public Task Import(BlockingCollection<ServerEvent> events)
        {
            var runnerTask = new Task(() => this.startReaderThreads(events));

            runnerTask.Start();

            return runnerTask;
        }

        private void startReaderThreads(BlockingCollection<ServerEvent> events)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                Task[] importerTasks = new Task[_maxReaderThreads];

                for (int i = 0; i < importerTasks.Length; i++)
                {
                    importerTasks[i] = new Task(() => this.startSingleThreadedImporting(events, db));
                    importerTasks[i].Start();
                }

                Task.WaitAll(importerTasks);
            }
        }

        private void startSingleThreadedImporting(BlockingCollection<ServerEvent> events, LiteDatabase db)
        {
            while (events.TryTake(out var srvEvent))
            {
                try
                {
                    var serverEvents = db.GetCollection<ServerEvent>("ServerEvents");
                    serverEvents.Insert(srvEvent);
                    serverEvents.EnsureIndex(e => e.Id);
                }
                catch (LiteException ex)
                {
                    // TODO: log here
                }
            }
        }
    }
}