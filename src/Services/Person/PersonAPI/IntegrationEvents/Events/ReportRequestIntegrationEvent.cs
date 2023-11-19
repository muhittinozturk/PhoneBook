﻿using EventBus.Event;

namespace PersonAPI.IntegrationEvents.Events
{
    public class ReportRequestIntegrationEvent : IntegrationEvent
    {
        public string ReportId { get; set; }
        public ReportRequestIntegrationEvent()
        {
        }
        public ReportRequestIntegrationEvent(string reportId)
        {
            ReportId = reportId;
        }
    }
}
