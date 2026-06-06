using Application.DTO;
using Domain.Entities.DetailedDesign;

namespace Application.Mappers;

public static class FuselageDtoMapper
{
    public static FuselageDto Map(FuselageEntity fuselage)
    {
        return new FuselageDto
        {
            Id = fuselage.Id.ToString(),
            Weight = $"{fuselage.Weight.Value} {fuselage.Weight.Unit}"
        };
    }
}
