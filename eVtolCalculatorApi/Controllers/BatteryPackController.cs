using Application.Queries.Battery;
using Application.Commands.Battery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BatteryPackController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<BatteryPackController> _logger;

    public BatteryPackController(ISender sender, ILogger<BatteryPackController> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    // GET api/<BatterPackController>/5
    [HttpGet("{id}")]
    [Route("GetBatteryPack")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetAsync)}BatteryController");
        var query = new GetBatteryPackByIdQuery(id);
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    // GET api/<BatterPackController>/5
    [HttpGet]
    [Route("GetBatteryPack")]
    public async Task<IActionResult> GetAsync(string name, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetAsync)} BatteryPack - {name}");
        var query = new GetBatteryPackByNameQuery(name);
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetBattery
    }

    // POST api/<BatterPackController>
    [HttpPost]
    [Route("CreateBatteryPack")]
    public async Task<IActionResult> PostAsync(CreateBatteryPackCommand command, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
    }
}
