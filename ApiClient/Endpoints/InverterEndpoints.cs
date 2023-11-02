using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
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
        var apiEndpoint = "/api/Inverter";

        var result = await _utilities.PostCommandAsync<CreateInverterModel, InverterDto>(blade, apiEndpoint);

        if (result is null)
        {
            return Result.Fail("We could not create the battery");
        }

        return result;
    }
}
