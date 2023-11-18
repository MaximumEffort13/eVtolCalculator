using Application.DTO;
using Domain.Entities;

namespace Application.Mappers;

public class DesignConstantsDtoMapper
{
    public static DesignConsantsDto Map(DesignConstantsEntity designConst)
    {
        return new DesignConsantsDto
        {
            Id = designConst.Id,
            Name = designConst.Name,
            Value = designConst.Value,
        };
    }
}
