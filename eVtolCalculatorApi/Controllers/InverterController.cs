using Application.InverterFacilitators.Commands;
using Application.InverterFacilitators.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InverterController : ControllerBase
{
    private readonly ISender _sender;

    public InverterController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<InverterController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetInverterByIdQuery(id);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    // POST api/<InverterController>
    [HttpPost]
    public async Task<IActionResult> Post(CreateInverterCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
    }
}
