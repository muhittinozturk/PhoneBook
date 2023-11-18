using EventBus.Abstractions;
using ReportAPI.Entities;
using ReportAPI.Enums;
using ReportAPI.IntegrationEvents.Events;
using ReportAPI.Repositories;

namespace ReportAPI.IntegrationEvents.IntegrationEvents
{
    public class ReportRequestResultIntegrationEventHandler : IIntegrationEventHandler<ReportRequestResultIntegrationEvent>
    {
        private readonly IReportRepository<Report> _reportRepository;

        public ReportRequestResultIntegrationEventHandler(IReportRepository<Report> reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Task Handle(ReportRequestResultIntegrationEvent @event)
        {
            var report = _reportRepository.GetById(@event.ReportId);
            if (report is null)
                throw new ArgumentException("Rapor bulunamadı");

            if (@event.IsSuccess)
            {
                report.Status = ReportStatus.Completed;
                _reportRepository.Update(report);
            }
            else
            {
                report.Status = ReportStatus.Failed;
            }

            return Task.CompletedTask;
        }
    }
}
