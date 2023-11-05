using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using ApiClient.Enums;
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
        var result = await _utilities.PostCommandAsync<CreateBladeModel, BladeDto>(blade, BladeRoutes.Create.Name);

        if (result is null || result.IsFailed)
        {

            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }

    public async Task<Result<List<BladeDto>>> GetBlades()
    {
        var results = await _utilities.GetRequestAsync<List<BladeDto>>(BladeRoutes.GetAll.Name);

        if (results is null || results.IsFailed)
        {
            return Result.Fail(results!.Reasons.Last().ToString());
        }

        return results;
    }
}