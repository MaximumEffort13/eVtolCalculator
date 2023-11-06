using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Motors;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Motors;
internal class GetMotorByNameQueryHandler : IQueryHandler<GetMotorByNameQuery, MotorDto>
{
    private readonly IMotorRepository _motorRepository;

    public GetMotorByNameQueryHandler(IMotorRepository motorRepository)
    {
        _motorRepository = motorRepository;
    }

    public async Task<Result<MotorDto>> Handle(GetMotorByNameQuery request, CancellationToken cancellationToken)
    {
        var motor = await _motorRepository.GetByNameAsync(request.Name, request.UserId, cancellationToken);

        var response = MotorDtoMapper.Map(motor);

        return response;
    }
}
