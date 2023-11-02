using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class BatteryPackEndpoints : IBatteryPackEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<BatteryPackEndpoints> _logger;

    public BatteryPackEndpoints(Utilities utilities, ILogger<BatteryPackEndpoints> logger)
    {
        _utilities = utilities;
        _logger = logger;
    }

    public async Task<Result<BatteryPackDto>> CreateBatteryPack(CreateBatteryModel battery)
    {
        var apiEndpoint = "/api/BatteryPack";

        var result = await _utilities.PostCommandAsync<CreateBatteryModel, BatteryPackDto>(battery, apiEndpoint);

        if (result is null)
        {
            return Result.Fail("We could not create the battery");
        }

        return result;
    }

    public async Task<Result<IEnumerable<BatteryPackDto>>> GetBatteries()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<BatteryPackDto>> GetBatteryById(Guid id)
    {
        throw new NotImplementedException();
    }
}
