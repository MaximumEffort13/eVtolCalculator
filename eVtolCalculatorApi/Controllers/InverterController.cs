using Application.Commands.Inverter;
using Application.Queries.Battery;
using Application.Queries.Inverter;
using eVtolCalculatorApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "user")]
public class InverterController : ControllerBase
{
    private readonly ISender _sender;

    public InverterController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<InverterController>/5
    [HttpGet]
    [Route("GetInverterById/{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetInverterByIdQuery(id, Guid.Parse(userId));

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // GET api/<InverterController>
    [HttpGet]
    [Route("GetInverterByName/{name}")]
    public async Task<IActionResult> GetAsync(string name, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetInverterByNameQuery(name, Guid.Parse(userId));

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

        var query = new GetAllInvertersQuery(Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }

    // POST api/<InverterController>
    [HttpPost]
    [Route("CreateInverter")]
    public async Task<IActionResult> Post(InverterInsert inverter, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        CreateInverterCommand command = new(Guid.Parse(userId), inverter);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }
}
