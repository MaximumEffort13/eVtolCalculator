using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using ApiClient.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class MotorEndpoints : IMotorEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<MotorEndpoints> _logger;
    private readonly ILoggedInUserModel _loggedInUserModel;

    public MotorEndpoints(Utilities utilities, ILogger<MotorEndpoints> logger, ILoggedInUserModel loggedInUserModel)
    {
        _utilities = utilities;
        _logger = logger;
        _loggedInUserModel = loggedInUserModel;
    }

    public async Task<Result<MotorDto>> CreateMotorAsync(CreateMotorModel motor)
    {        
        var result = await _utilities.PostCommandAsync<CreateMotorModel, MotorDto>(motor, MotorRoutes.Create.Name);

        if (result is null || result.IsFailed)
        {
            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }

    public async Task<Result<List<MotorDto>>> GetMotors()
    {
        var results = await _utilities.GetRequestAsync<List<MotorDto>>(MotorRoutes.GetAll.Name);

        if (results is null || results.IsFailed)
        {
            return Result.Fail(results!.Reasons.Last().ToString());
        }

        return results;
    }

    public async Task<Result<MotorDto>> GetMotorById(string id)
    {
        var results = await _utilities.GetRequestAsync<MotorDto>($"{MotorRoutes.GetById.Name}/{id}");

        if (results is null || results.IsFailed)
        {
            return Result.Fail("We could not retrieve the requested data.");
        }

        return results;
    }
}
