using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Inverter;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Inverter;

internal sealed class GetAllInvertersQueryHandler : IQueryHandler<GetAllInvertersQuery, List<InverterDto>>
{
    private readonly IInvererRepository _inverterRepository;

    public GetAllInvertersQueryHandler(IInvererRepository inverterRepository)
    {
        _inverterRepository = inverterRepository;
    }

    public async Task<Result<List<InverterDto>>> Handle(GetAllInvertersQuery request, CancellationToken cancellationToken)
    {
        var inverters = await _inverterRepository.GetAllAsync(request.UserId, cancellationToken);

        List<InverterDto> inverterDtos = new ();

        inverters.ForEach(inverter =>
        {
            inverterDtos.Add(InverterDtoMapper.Map(inverter));
        });

        return inverterDtos;
    }
}
