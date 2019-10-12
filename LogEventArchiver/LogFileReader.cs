﻿using LogEventArchiver.Models;

using Newtonsoft.Json.Linq;

using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LogEventArchiver
{
    public class LogFileReader : ILogFileReader
    {
        private string _logFile;
        private long _alertThreshold;
        private ConcurrentDictionary<string, ServerEventDto> _tempSavedEvents;

        public LogFileReader(string logFile, long alertThreshold)
        {
            _logFile = logFile;
            _alertThreshold = alertThreshold;
            _tempSavedEvents = new ConcurrentDictionary<string, ServerEventDto>();
        }

        public Task ReadAllEvents(BlockingCollection<ServerEvent> events)
        {
            var runnerTask = new Task(() =>
            {
                using (var file = File.OpenRead(_logFile))
                using (var reader = new StreamReader(file, Encoding.ASCII))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var srvEventDto = ServerEventDtoFactory.GetServerEventDto(line);

                        if (this.tryGetPairedEvent(srvEventDto, out var pairedSrvEventDto))
                        {
                            var srvEvent = ServerEventFactory.GetServerEvent(srvEventDto, pairedSrvEventDto, _alertThreshold);
                            events.Add(srvEvent);
                        }
                    }
                }
            });

            runnerTask.Start();

            return runnerTask;
        }

        private bool tryGetPairedEvent(ServerEventDto eventDto, out ServerEventDto pairedEvent)
        {
            if (_tempSavedEvents.TryRemove(eventDto.Id, out pairedEvent))
                return true;

            if (!_tempSavedEvents.TryAdd(eventDto.Id, eventDto))
            {
                if (_tempSavedEvents.TryRemove(eventDto.Id, out pairedEvent))
                    return true;
            }

            return false;
        }
    }
}