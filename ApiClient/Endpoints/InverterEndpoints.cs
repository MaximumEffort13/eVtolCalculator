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

    public InverterEndpoints(Utilities utilities, ILogger<InverterEndpoints> logger)
    {
        _utilities = utilities;
        _logger = logger;
    }

    public async Task<Result<InverterDto>> CreateInverterAsync(CreateInverterModel blade)
    {
        var result = await _utilities.PostCommandAsync<CreateInverterModel, InverterDto>(blade, InverterRoutes.Create.Name);

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
}
