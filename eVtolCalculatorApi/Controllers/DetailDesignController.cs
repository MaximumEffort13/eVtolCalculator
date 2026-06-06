using Application.Commands.Battery;
using Application.Commands.Blade;
using Application.Commands.DetailDesign;
using Application.Commands.Fuselage;
using Application.Commands.Inverter;
using Application.Commands.Mission;
using Application.Commands.Motors;
using Application.Queries.DetailDesign;
using eVtolCalculatorApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "user")]
public class DetailDesignController : ControllerBase
{
    private readonly ISender _sender;

    public DetailDesignController(ISender sender)
    {
        _sender = sender;
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

        var query = new GetAllDetailDesignsQuery(Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // GET api/<DetailDesignController>/5
    [HttpGet]
    [Route("GetDetailDesignById/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetDetailDesignByIdQuery(id, Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
    }

    // GET api/<DetailDesignController>/name
    [HttpGet]
    [Route("GetDetailDesignByName/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        var query = new GetDetailDesignByNameQuery(name, Guid.Parse(userId));
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

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return BadRequest();
        }

        Guid parsedUserId = Guid.Parse(userId);

        CreateBatteryPackCommand batteryCommand = new(parsedUserId, inputCommands.Battery);
        CreateMotorCommand motorCommand = new(parsedUserId, inputCommands.Motor);
        CreateInverterCommand inverterCommand = new(parsedUserId, inputCommands.Inverter);
        CreateFuselageCommand fuselageCommand = new(parsedUserId, inputCommands.Fuselage);
        CreateBladeCommand bladeCommand = new(parsedUserId, inputCommands.Blade);
        CreateMissionParameterCommand missionCommand = new(parsedUserId, inputCommands.Mission);

        var battery = await _sender.Send(batteryCommand, cancellationToken);
        var motor = await _sender.Send(motorCommand, cancellationToken);
        var inverter = await _sender.Send(inverterCommand, cancellationToken);
        var fuselage = await _sender.Send(fuselageCommand, cancellationToken);
        var blade = await _sender.Send(bladeCommand, cancellationToken);
        var mission = await _sender.Send(missionCommand, cancellationToken);

        var electricVtolCommand = new CreateDetailedDesignCommand(
            parsedUserId,
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
    public async Task<IActionResult> Post(CreateDetailDesignGuidLinkInput input, CancellationToken cancellationToken)
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

        CreateFuselageCommand fuselageCommand = new(Guid.Parse(userId), input.Fuselage);
        CreateMissionParameterCommand missionCommand = new(Guid.Parse(userId), input.MissionParameters);

        var fuselage = await _sender.Send(fuselageCommand, cancellationToken);
        var mission = await _sender.Send(missionCommand, cancellationToken);

        var electricVtolCommand = new CreateDetailDesignWithGuidLinksCommand(
            Guid.Parse(userId),
            input.Name,
            Guid.Parse(input.BatteryPackId),
            Guid.Parse(input.MotorId),
            Guid.Parse(input.InverterId),
            Guid.Parse(input.BladeId),
            Guid.Parse(mission.Value.Id),
            Guid.Parse(fuselage.Value.Id),
            input.MotorQuantity,
            input.BladePerMotorQuantity);

        var response = await _sender.Send(electricVtolCommand, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
    }
}
