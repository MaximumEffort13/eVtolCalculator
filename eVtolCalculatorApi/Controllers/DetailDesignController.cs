using Application.DetailDesignFacilitators.Commands;
using Application.DetailDesignFacilitators.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DetailDesignController : ControllerBase
{
    private readonly ISender _sender;

    public DetailDesignController(ISender sender)
    {
        _sender = sender;
    }

    // GET api/<DetailDesignController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetDetailDesignByIdQuery(id);
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : NotFound(response.Errors);
    }

    // POST api/<DetailDesignController>
    [HttpPost]
    public async Task<IActionResult> Post(InsertDetailDesignDto inputCommands, CancellationToken cancellationToken)
    {
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

        return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
    }
}
