using Application.Queries.Blade;
using Application.Commands.Blade;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.Battery;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
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
        var query = new GetBladeByIdQuery(id);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response);
    }

    // GET api/<Blade>/
    [HttpGet]
    [Route("GetBladeByName")]
    public async Task<IActionResult> GetByNameAsync([FromBody] string name, CancellationToken cancellationToken)
    {
        var query = new GetBladeByNameQuery(name);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response);
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllBladesQuery();
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }

    // POST api/<Blade>
    [HttpPost]
    [Route("CreateBlade")]
    public async Task<IActionResult> Post(CreateBladeCommand command, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response);
    }
}
