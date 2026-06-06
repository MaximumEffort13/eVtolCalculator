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
    private readonly ILoggedInUserModel _loggedInUserModel;

    public BladeEndpoints(Utilities utilities, ILogger<BladeEndpoints> logger, ILoggedInUserModel loggedInUserModel)
    {
        _utilities = utilities;
        _logger = logger;
        _loggedInUserModel = loggedInUserModel;
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

    public async Task<Result<BladeDto>> GetBladeById(string id)
    {
        var results = await _utilities.GetRequestAsync<BladeDto>($"{BladeRoutes.GetById.Name}/{id}");

        if (results is null || results.IsFailed)
        {
            return Result.Fail("We could not retrieve the requested data.");
        }

        return results;
    }
}