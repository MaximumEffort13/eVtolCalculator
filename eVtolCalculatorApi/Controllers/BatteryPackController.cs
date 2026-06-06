using Application.Queries.Battery;
using Application.Commands.Battery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eVtolCalculatorApi.Models;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.AuthenticationModels;
using System.Security.Claims;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy ="user")]
public class BatteryPackController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<BatteryPackController> _logger;
    private readonly UserManager<IdentityUserExtender> _userManager;

    public BatteryPackController(ISender sender, ILogger<BatteryPackController> logger, UserManager<IdentityUserExtender> userManager)
    {
        _sender = sender;
        _logger = logger;
        _userManager = userManager;
    }

    // GET api/<BatterPackController>/5
    [HttpGet]
    [Route("GetBatteryPackById/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetByIdAsync)}BatteryController");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetBatteryPackByIdQuery(id, Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // GET api/<BatterPackController>/5
    [HttpGet]
    [Route("GetBatteryPackByName/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetByNameAsync)} BatteryPack - {name}");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetBatteryPackByNameQuery(name, Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetAllBatteryPacksQuery(Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }

    // POST api/<BatterPackController>
    [HttpPost]
    [Route("CreateBatteryPack")]
    public async Task<IActionResult> PostAsync(BatteryPackInsert battery, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        CreateBatteryPackCommand command = new(Guid.Parse(userId), battery);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }
}
