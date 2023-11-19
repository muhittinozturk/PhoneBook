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
            var response = await _mediator.Send(new GetReportQueryRequest() { ReportDetailId = id });

            return Ok(response);
        }

        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            List<GetReportDetail> response = await _mediator.Send(new GetAllReportQueryRequest() { ReportId = id});

            return Ok(response);
        }

        
    }
}
