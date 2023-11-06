using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.ConceptualDesign;
using Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Application.Commands.ConceptualDesign;
using Application.Queries.Battery;
using eVtolCalculatorApi.Models;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "user")]

public class ConceptualDesignController : ControllerBase
{
    private readonly ISender _sender;

    public ConceptualDesignController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<ConceptualDesignController>/5
    [HttpGet]
    [Route("GetConceptualDesignById/{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetConceptualDesignByIdQuery(id, Guid.Parse(userId));
        Result<ConceptualDesignDto> response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess? Ok(response.Value) : NotFound(response.Errors);
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

        var query = new GetAllConceptualDesignsQuery(Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }

    // POST api/<ConceptualDesignController>
    [HttpPost]
    [Route("CreateConceptualDesign")]
    public async Task<IActionResult> PostAsync(ConceptualDesignInsert conceptInsert, CancellationToken cancellationToken)
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

        CreateConceptualDesignCommand command = new CreateConceptualDesignCommand(Guid.Parse(userId), conceptInsert);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }
}
