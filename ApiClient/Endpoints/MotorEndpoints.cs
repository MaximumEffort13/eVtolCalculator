using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class MotorEndpoints : IMotorEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<MotorEndpoints> _logger;

    public MotorEndpoints(Utilities utilities, ILogger<MotorEndpoints> logger)
    {
        _utilities = utilities;
        _logger = logger;
    }

    public async Task<Result<MotorDto>> CreateMotorAsync(CreateMotorModel blade)
    {
        var apiEndpoint = "/api/Motor";

        var result = await _utilities.PostCommandAsync<CreateMotorModel, MotorDto>(blade, apiEndpoint);

        if (result is null)
        {
            return Result.Fail("We could not create the battery");
        }

        return result;
    }
}
