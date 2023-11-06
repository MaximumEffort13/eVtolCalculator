using Application.Queries.Blade;
using Application.Commands.Blade;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.Battery;
using eVtolCalculatorApi.Models;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "user")]
public class BladeController : ControllerBase
{
    private readonly ISender _sender;

    public BladeController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<Blade>/5
    [HttpGet]
    [Route("GetBladeById/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetBladeByIdQuery(id, Guid.Parse(userId));

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response);
    }

    // GET api/<Blade>/
    [HttpGet]
    [Route("GetBladeByName/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetBladeByNameQuery(name, Guid.Parse(userId));

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response);
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

        var query = new GetAllBladesQuery(Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }

    // POST api/<Blade>
    [HttpPost]
    [Route("CreateBlade")]
    public async Task<IActionResult> Post(BladeInsert blade, CancellationToken cancellationToken)
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

        CreateBladeCommand command = new(Guid.Parse(userId), blade);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response);
    }
}
