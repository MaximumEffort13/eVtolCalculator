using Application.DTO;
using Domain.Entities.DetailedDesign;

namespace Application.Mappers;

public static class FuselageDtoMapper
{
    public static FuselageDto Map(Fuselage fuselage)
    {
        return new FuselageDto
        {
            Id = fuselage.Id,
            Weight = $"{fuselage.Weight.Value} {fuselage.Weight.Unit}"
        };
    }
}
