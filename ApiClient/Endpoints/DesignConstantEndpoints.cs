using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using ApiClient.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class DesignConstantsEndpoints(Utilities utilities, ILogger<DesignConstantsEndpoints> logger, ILoggedInUserModel loggedInUserModel) : IDesignConstantsEndpoints
{
    private readonly Utilities _utilities = utilities;
    private readonly ILogger<DesignConstantsEndpoints> _logger = logger;
    private readonly ILoggedInUserModel _loggedInUserModel = loggedInUserModel;

    public async Task<Result<DesignConstantsDto>> CreateDesingConstants(CreateDesignConstants designConstant)
    {
        var result = await _utilities.PostCommandAsync<CreateDesignConstants, DesignConstantsDto>(designConstant, DesignConstantRoutes.Create.Name);

        if (result is null || result.IsFailed)
        {

            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }

    public async Task<Result<List<DesignConstantsDto>>> GetAllDesignConstants()
    {
        var results = await _utilities.GetRequestAsync<List<DesignConstantsDto>>(DesignConstantRoutes.GetAll.Name);

        if (results is null || results.IsFailed)
        {
            return Result.Fail(results!.Reasons.Last().ToString());
        }

        return results;
    }

    public async Task<Result<DesignConstantsDto>> GetDesignConstantById(string id)
    {
        var results = await _utilities.GetRequestAsync<DesignConstantsDto>($"{DesignConstantRoutes.GetById.Name}/{id}");

        if (results is null || results.IsFailed)
        {
            return Result.Fail("We could not retrieve the requested data.");
        }

        return results;
    }
}