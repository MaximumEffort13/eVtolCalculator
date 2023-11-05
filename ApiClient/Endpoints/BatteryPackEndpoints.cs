using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using ApiClient.Enums;
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
        var result = await _utilities.PostCommandAsync<CreateBatteryModel, BatteryPackDto>(battery, BatteryPackRoutes.Create.Name);

        if (result is null)
        {
            return Result.Fail("We could not create the battery");
        }

        return result;
    }

    public async Task<Result<List<BatteryPackDto>>> GetBatteries()
    {
        var results = await _utilities.GetRequestAsync<List<BatteryPackDto>>(BatteryPackRoutes.GetAll.Name);

        if (results is null || results.IsFailed)
        {
            return Result.Fail("We could not retrieve the requested data.");
        }

        return results;
    }

    public async Task<Result<BatteryPackDto>> GetBatteryById(Guid id)
    {
        throw new NotImplementedException();
    }
}
