using EventBus.Event;

namespace ReportAPI.IntegrationEvents.Events
{
    public class ReportRequestResultIntegrationEvent : IntegrationEvent
    {
        public Guid ReportId { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public ReportRequestResultIntegrationEvent()
        {
        }
        public ReportRequestResultIntegrationEvent(Guid reportId, string message, bool isSuccess)
        {
            ReportId = reportId;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
