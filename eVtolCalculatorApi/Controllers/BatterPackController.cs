using Application.BatteryFacilitators.Commands;
using Application.BatteryFacilitators.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BatterPackController : ControllerBase
{
    private readonly ISender _sender;

    public BatterPackController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<BatterPackController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBatteryPackByIdQuery(id);
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    // GET api/<BatterPackController>/5
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromBody] string name, CancellationToken cancellationToken)
    {
        var query = new GetBatteryPackByNameQuery(name);
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    // POST api/<BatterPackController>
    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateBatteryPackCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
    }
}
