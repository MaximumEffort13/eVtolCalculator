using Application.Commands.DesignConstants;
using Application.Queries.DesignConstants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Policy = "admin")]
public class DesignConstantsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    // GET: api/<DesignConstantsController>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllDesignConstantsQuery();
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest();
    }

    // GET api/<DesignConstantsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetDesignConstantByIdQuery(id);
        var resonse = await _sender.Send(query, cancellationToken);

        return resonse.IsSuccess ? Ok(resonse.Value) : BadRequest();
    }

    // POST api/<DesignConstantsController>
    [HttpPost]
    public async Task<IActionResult> CreateDesignConstant(CreateDesignConstantsCommand command, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors.Last().Message);
    }
}
