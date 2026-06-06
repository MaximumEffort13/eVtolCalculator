using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DesignConstants;
using Domain.Abstractions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.DesignConstants;

internal class GetAllDesignConstantsQueryHandler(IDesignConstantsRepository designConstants) : IQueryHandler<GetAllDesignConstantsQuery, List<DesignConsantsDto>>
{
    private readonly IDesignConstantsRepository _designConstants = designConstants;

    public async Task<Result<List<DesignConsantsDto>>> Handle(GetAllDesignConstantsQuery request, CancellationToken cancellationToken)
    {
        var designConstants = await _designConstants.GetAllAsync(cancellationToken);

        List<DesignConsantsDto> designConstantDtos = new ();

        designConstants.ForEach(a => designConstantDtos.Add(DesignConstantsDtoMapper.Map(a)));

        return designConstantDtos;
    }
}
