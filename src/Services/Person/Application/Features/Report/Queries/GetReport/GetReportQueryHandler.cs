using Application.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Report.Queries.GetReport
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQueryRequest, GetReportQueryResponse>
    {
        private readonly IReportService _reportService;

        public GetReportQueryHandler(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<GetReportQueryResponse> Handle(GetReportQueryRequest request, CancellationToken cancellationToken)
        {
            var reportDetail = await _reportService.GetReportDetailById(request.ReportDetailId);

            return new()
            {
                PhoneNumberCount = reportDetail.PhoneNumberCount,
                PersonCount = reportDetail.PersonCount,
                Location = reportDetail.Location,
            };
        }
    }
}
