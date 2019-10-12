using System;
using LogEventArchiver.Models;

namespace LogEventArchiver
{
    public static class ServerEventFactory
    {
        public static ServerEvent GetServerEvent(ServerEventDto srvEventDto1, ServerEventDto srvEventDto2, long alertThreshold)
        {
            long duration = Math.Abs(srvEventDto1.Timestamp - srvEventDto2.Timestamp);
            bool alert = duration > alertThreshold;

            return string.IsNullOrEmpty(srvEventDto1.Type)
                ? ServerEventFactory.createBaseServerEvent(srvEventDto1, srvEventDto2, duration, alert)
                : ServerEventFactory.createAppServerEvent(srvEventDto1, srvEventDto2, duration, alert);
        }

        private static ServerEvent createBaseServerEvent(ServerEventDto srvEventDto1, ServerEventDto srvEventDto2, long duration, bool alert)
        {
            return new ServerEvent
            {
                Id = srvEventDto1.Id,
                Duration = duration,
                Alert = alert
            };
        }

        private static ServerEvent createAppServerEvent(ServerEventDto srvEventDto1, ServerEventDto srvEventDto2, long duration, bool alert)
        {
            return new AppServerEvent
            {
                Id = srvEventDto1.Id,
                Duration = duration,
                Alert = alert,
                Type = srvEventDto1.Type,
                Host = srvEventDto1.Host
            };
        }
    }
}