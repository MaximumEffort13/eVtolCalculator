using Application.Commands.DetailDesign;
using Application.Queries.DetailDesign;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class DetailDesignController : ControllerBase
{
    private readonly ISender _sender;

    public DetailDesignController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllDetailDesignsQuery();
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // GET api/<DetailDesignController>/5
    [HttpGet]
    [Route("GetDetailDesignById/{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var query = new GetDetailDesignByIdQuery(Guid.Parse(id));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // GET api/<DetailDesignController>/name
    [HttpGet]
    [Route("GetDetailDesignByName/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var query = new GetDetailDesignByNameQuery(name);
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // POST api/<DetailDesignController>
    [HttpPost]
    [Route("CreateFullNewDetailDesign")]
    public async Task<IActionResult> Post(InsertDetailDesignDto inputCommands, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        var battery = await _sender.Send(inputCommands.Battery, cancellationToken);
        var motor = await _sender.Send(inputCommands.Motor, cancellationToken);
        var inverter = await _sender.Send(inputCommands.Inverter, cancellationToken);
        var fuselage = await _sender.Send(inputCommands.Fuselage, cancellationToken);
        var blade = await _sender.Send(inputCommands.Blade, cancellationToken);
        var mission = await _sender.Send(inputCommands.Mission, cancellationToken);

        var electricVtolCommand = new CreateDetailedDesignCommand(
            inputCommands.Name,
            battery.Value,
            inverter.Value,
            motor.Value,
            blade.Value,
            fuselage.Value,
            mission.Value,
            inputCommands.MotorQuantity,
            inputCommands.BladePerMotorCount,
            inputCommands.Mission.PayloadWeight_kg,
            inputCommands.Mission.FlightTimeInMinutes);

        var response = await _sender.Send(electricVtolCommand, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }

    // POST api/<DetailDesignController>
    [HttpPost]
    [Route("CreateFullNewDetailDesignWithGuidLinks")]
    public async Task<IActionResult> Post(CreateDetailDesignGuidLinkInput inputCommands, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }
        var fuselage = await _sender.Send(inputCommands.Fuselage, cancellationToken);
        var mission = await _sender.Send(inputCommands.MissionParameters, cancellationToken);

        var electricVtolCommand = new CreateDetailDesignWithGuidLinksCommand(
            inputCommands.Name,
            Guid.Parse(inputCommands.BatteryPackId),
            Guid.Parse(inputCommands.MotorId),
            Guid.Parse(inputCommands.InverterId),
            Guid.Parse(inputCommands.BladeId),
            Guid.Parse(mission.Value.Id),
            Guid.Parse(fuselage.Value.Id),
            inputCommands.MotorQuantity,
            inputCommands.BladePerMotorQuantity);

        var response = await _sender.Send(electricVtolCommand, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }
}
