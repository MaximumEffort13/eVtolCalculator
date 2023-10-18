using Application.BladeFacilitators.Commands;
using Application.BladeFacilitators.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BladeController : ControllerBase
{
    private readonly ISender _sender;

    public BladeController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<BladeController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBladeByIdQuery(id);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    // GET api/<BladeController>/5
    [HttpGet]
    public async Task<IActionResult> Get([FromBody] string name, CancellationToken cancellationToken)
    {
        var query = new GetBladeByNameQuery(name);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    // POST api/<BladeController>
    [HttpPost]
    public async Task<IActionResult> Post(CreateBladeCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
