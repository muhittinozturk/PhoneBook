using EventBus.Event;

namespace PersonAPI.IntegrationEvents.Events
{
    public class ReportRequestIntegrationEvent : IntegrationEvent
    {
        public Guid ReportId { get; set; }
        public ReportRequestIntegrationEvent()
        {
        }
        public ReportRequestIntegrationEvent(Guid reportId)
        {
            ReportId = reportId;
        }
    }
}
