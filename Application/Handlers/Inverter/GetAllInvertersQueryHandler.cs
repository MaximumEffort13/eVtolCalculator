using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Inverter;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Inverter;

internal sealed class GetAllInvertersQueryHandler : IQueryHandler<GetAllInvertersQuery, IEnumerable<InverterDto>>
{
    private readonly IInvererRepository _inverterRepository;

    public GetAllInvertersQueryHandler(IInvererRepository inverterRepository)
    {
        _inverterRepository = inverterRepository;
    }

    public async Task<Result<IEnumerable<InverterDto>>> Handle(GetAllInvertersQuery request, CancellationToken cancellationToken)
    {
        var inverters = await _inverterRepository.GetAllAsync(cancellationToken);

        List<InverterDto> inverterDtos = new List<InverterDto>();

        inverters.ForEach(inverter =>
        {
            inverterDtos.Add(InverterDtoMapper.Map(inverter));
        });

        return inverterDtos;
    }
}
