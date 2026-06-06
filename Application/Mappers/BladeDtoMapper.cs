using Application.DTO;
using Domain.Entities.DetailedDesign;

namespace Application.Mappers;

public static class BladeDtoMapper
{
    public static BladeDto Map(BladeEntity blade)
    {
        return new BladeDto
        {
            Id = blade.Id.ToString(),
            Name = blade.Name,
            Diameter = $"{blade.Diameter.Value} {blade.Diameter.Unit}",
            Width = $"{blade.Width.Value} {blade.Width.Unit}",
            Thickness = $"{blade.Thickness.Value} {blade.Thickness.Unit}",
            Weight = $"{blade.Weight.Value} {blade.Weight.Unit}",
            AngleOfAttack = $"{blade.AngleOfAttack.Value} {blade.AngleOfAttack.Unit}",
        };
    }
}
