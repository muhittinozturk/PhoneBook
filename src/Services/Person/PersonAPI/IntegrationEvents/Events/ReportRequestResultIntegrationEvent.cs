using EventBus.Event;

namespace PersonAPI.IntegrationEvents.Events
{
    public class ReportRequestResultIntegrationEvent : IntegrationEvent
    {
        public string ReportId { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public ReportRequestResultIntegrationEvent()
        {
        }
        public ReportRequestResultIntegrationEvent(string reportId, string message, bool isSuccess)
        {
            ReportId = reportId;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
