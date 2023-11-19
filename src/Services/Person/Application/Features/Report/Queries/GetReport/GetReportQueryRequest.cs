using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Report.Queries.GetReport
{
    public class GetReportQueryRequest : IRequest<GetReportQueryResponse>
    {
        public Guid ReportDetailId { get; set; }
    }
}
