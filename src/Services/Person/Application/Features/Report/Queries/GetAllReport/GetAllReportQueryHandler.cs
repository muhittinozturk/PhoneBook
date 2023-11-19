using Application.Abstract;
using Application.Dtos;
using Application.Features.Report.Queries.GetReport;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Report.Queries.GetAllReport
{
    public class GetAllReportQueryHandler : IRequestHandler<GetAllReportQueryRequest, List<GetReportDetail>>
    {
        private readonly IReportService _reportService;

        public GetAllReportQueryHandler(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<List<GetReportDetail>> Handle(GetAllReportQueryRequest request, CancellationToken cancellationToken)
        {

            return await _reportService.GetAllReportDetail(request.ReportId);
        }
    }
}
