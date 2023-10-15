using Application.DTO;
using Application.MotorFacilitators.Commands;
using Application.MotorFacilitators.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MotorController : ControllerBase
{
    private readonly ISender _sender;

    public MotorController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<MotorController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMotorByIdQuery(id);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    // POST api/<MotorController>
    [HttpPost]
    public async Task<IActionResult> Post(CreateMotorCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
    }
}
