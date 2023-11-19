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

        public async Task Handle(ReportRequestResultIntegrationEvent @event)
        {
            var report = await _reportRepository.GetByIdAsync(x => x.Id == @event.ReportId);
            if (report is null)
                throw new ArgumentException("Rapor bulunamadı");

            if (@event.IsSuccess)
            {
                report.Status = ReportStatus.Completed;
            }
            else
            {
                report.Status = ReportStatus.Failed;
            };

            var updateEntity = await _reportRepository.UpdateAsync(report, x => x.Id == @event.ReportId);

        }
    }
}
