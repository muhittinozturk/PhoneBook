using Application.Features.Person.Commands.AddPerson;
using Application.Features.Person.Commands.DeletePerson;
using Application.Features.Person.Commands.UpdatePerson;
using Application.Features.Person.Queries.GetPerson;
using Application.Features.Person.Queries.GetPersonList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PersonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {   GetPersonQueryRequest request = new GetPersonQueryRequest();
            request.PersonId = id;
            GetPersonQueryResponse response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<GetPersonListQueryResponse> response = await _mediator.Send(new GetPersonListQueryRequest());

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPersonCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? StatusCode((int)HttpStatusCode.Created) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePersonCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? StatusCode((int)HttpStatusCode.Created) : BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeletePersonCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? StatusCode((int)HttpStatusCode.Created) : BadRequest();
        }
    }
}
