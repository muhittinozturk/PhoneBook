using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Report.Commands.CreateReport
{
    public class CreateReportCommandRequest : IRequest<CreateReportDto>
    {
        public Guid ReportId { get; set; }
    }
}
