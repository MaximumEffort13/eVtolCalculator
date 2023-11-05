using Application.Commands.Motors;
using Application.Queries.Battery;
using Application.Queries.Motors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class MotorController : ControllerBase
{
    private readonly ISender _sender;

    public MotorController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<Motor>/5
    [HttpGet]
    [Route("GetMotorById/{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMotorByIdQuery(id);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // GET api/<Motor>
    [HttpGet]
    [Route("GetMotorByName/{name}")]
    public async Task<IActionResult> GetAsync(string name, CancellationToken cancellationToken)
    {
        var query = new GetMotorByNameQuery(name);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllMotorsQuery();
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }

    // POST api/<Motor>
    [HttpPost]
    [Route("CreateMotor")]
    public async Task<IActionResult> Post(CreateMotorCommand command, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }
}
