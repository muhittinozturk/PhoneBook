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
    public class GetAllReportQueryRequest : IRequest<List<GetReportDetail>>
    {
        public Guid ReportId { get; set; }
    }
}
