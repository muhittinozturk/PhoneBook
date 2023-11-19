using Application.Features.Report.Commands.CreateReport;
using EventBus.Abstractions;
using EventBus.Event;
using MediatR;
using PersonAPI.IntegrationEvents.Events;

namespace PersonAPI.IntegrationEvents.IntegrationEvents
{
    public class ReportRequestIntegrationEventHandler : IIntegrationEventHandler<ReportRequestIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IMediator _mediator;

        public ReportRequestIntegrationEventHandler(IMediator mediator, IEventBus eventBus)
        {
            _mediator = mediator;
            _eventBus = eventBus;
        }

        public async Task Handle(ReportRequestIntegrationEvent @event)
        {
            CreateReportCommandRequest request = new CreateReportCommandRequest();
            request.ReportId = @event.ReportId;

            var response = await _mediator.Send(request);

            IntegrationEvent integrationEvent = new ReportRequestResultIntegrationEvent(response.ReportId, response.Message, response.IsSuccess);

            _eventBus.Publish(integrationEvent);
        }
    }
}
