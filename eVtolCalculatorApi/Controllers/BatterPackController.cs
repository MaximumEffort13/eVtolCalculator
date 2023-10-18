using Application.BatteryFacilitators.Commands;
using Application.BatteryFacilitators.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BatterPackController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<BatterPackController> _logger;

    public BatterPackController(ISender sender, ILogger<BatterPackController> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    // GET api/<BatterPackController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetAsync)}BatteryController");
        var query = new GetBatteryPackByIdQuery(id);
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    // GET api/<BatterPackController>/5
    [HttpGet]
    public async Task<IActionResult> GetAsync(string name, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetAsync)} BatteryPack - {name}");
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
