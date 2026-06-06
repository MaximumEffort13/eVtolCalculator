using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using ApiClient.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class InverterEndpoints : IInverterEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<InverterEndpoints> _logger;
    private readonly ILoggedInUserModel _loggedInUserModel;

    public InverterEndpoints(Utilities utilities, ILogger<InverterEndpoints> logger, ILoggedInUserModel loggedInUserModel)
    {
        _utilities = utilities;
        _logger = logger;
        _loggedInUserModel = loggedInUserModel;
    }

    public async Task<Result<InverterDto>> CreateInverterAsync(CreateInverterModel inverter)
    {
        var result = await _utilities.PostCommandAsync<CreateInverterModel, InverterDto>(inverter, InverterRoutes.Create.Name);

        if (result is null)
        {
            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }

    public async Task<Result<List<InverterDto>>> GetInverters()
    {
        var results = await _utilities.GetRequestAsync<List<InverterDto>>(InverterRoutes.GetAll.Name);

        if (results is null || results.IsFailed)
        {
            return Result.Fail(results!.Reasons.Last().ToString());
        }

        return results;
    }

    public async Task<Result<InverterDto>> GetInverterById(string id)
    {
        var results = await _utilities.GetRequestAsync<InverterDto>($"{InverterRoutes.GetById.Name}/{id}");

        if (results is null || results.IsFailed)
        {
            return Result.Fail("We could not retrieve the requested data.");
        }

        return results;
    }
}
