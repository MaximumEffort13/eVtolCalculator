using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using ApiClient.Enums;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class DetailDesignEndpoints : IDetailDesignEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<DetailDesignEndpoints> _logger;

    public DetailDesignEndpoints(Utilities utilities, ILogger<DetailDesignEndpoints> logger)
    {
        _utilities = utilities;
        _logger = logger;
    }

    public async Task<Result<ElectricVtolDesignDto>> CreateElectricVtolDesign(CreateDetailedDesign detailedDesign)
    {
        var result = await _utilities.PostCommandAsync<CreateDetailedDesign, ElectricVtolDesignDto>(detailedDesign, DetailDesignRoutes.Create.Name);

        if (result.IsFailed)
        {
            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }

    public async Task<Result<List<ElectricVtolDesignDto>>> GetAllDetailDesigns()
    {
        var result = await _utilities.GetRequestAsync<List<ElectricVtolDesignDto>>(DetailDesignRoutes.GetAll.Name);

        if (result.IsFailed)
        {
            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }

    public async Task<Result<ElectricVtolDesignDto>> GetDetailDesignByIdAsync(string id)
    {
        var result = await _utilities.GetRequestAsync<ElectricVtolDesignDto>($"{DetailDesignRoutes.GetById.Name}/{id}");

        if (result.IsFailed)
        {
            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }
}
