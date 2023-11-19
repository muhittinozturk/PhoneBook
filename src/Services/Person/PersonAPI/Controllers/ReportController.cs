using Application.Dtos;
using Application.Features.Person.Commands.AddPerson;
using Application.Features.Person.Commands.DeletePerson;
using Application.Features.Report.Queries.GetAllReport;
using Application.Features.Report.Queries.GetReport;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PersonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            GetReportQueryRequest request = new GetReportQueryRequest();
            request.ReportDetailId = id;
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<GetReportDetail> response = await _mediator.Send(new GetAllReportQueryRequest());

            return Ok(response);
        }

        
    }
}
