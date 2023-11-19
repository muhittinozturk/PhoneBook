using Application.Abstract;
using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Report.Commands.CreateReport
{
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommandRequest, CreateReportDto>
    {
        private readonly IReportService _reportService;

        public CreateReportCommandHandler(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<CreateReportDto> Handle(CreateReportCommandRequest request, CancellationToken cancellationToken)
        {

            return await _reportService.GenerateReportAsync(request.ReportId);            
        }
    }
}
