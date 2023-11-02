using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class BladeEndpoints : IBladeEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<BladeEndpoints> _logger;

    public BladeEndpoints(Utilities utilities, ILogger<BladeEndpoints> logger)
    {
        _utilities = utilities;
        _logger = logger;
    }

    public async Task<Result<BladeDto>> CreateBladeAsync(CreateBladeModel blade)
    {
        var apiEndpoint = "/api/Blade";

        var result = await _utilities.PostCommandAsync<CreateBladeModel, BladeDto>(blade, apiEndpoint);

        if (result is null)
        {
            return Result.Fail("We could not create the battery");
        }

        return result;
    }
}