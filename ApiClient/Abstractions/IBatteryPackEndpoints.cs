using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;

namespace ApiClient.Abstractions;

public interface IBatteryPackEndpoints
{
    Task<Result<BatteryPackDto>> CreateBatteryPack(CreateBatteryModel battery);
    Task<Result<List<BatteryPackDto>>> GetBatteries();
    Task<Result<BatteryPackDto>> GetBatteryByIdAsync(string id);
}