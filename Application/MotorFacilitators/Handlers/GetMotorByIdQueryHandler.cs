using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.MotorFacilitators.Queries;
using Domain.Abstractions;
using FluentResults;

namespace Application.MotorFacilitators.Handlers;
internal class GetMotorByIdQueryHandler : IQueryHandler<GetMotorByIdQuery, MotorDto>
{
    private readonly IMotorRepository _motorRepository;

    public GetMotorByIdQueryHandler(IMotorRepository motorRepository)
    {
        _motorRepository = motorRepository;
    }

    public async Task<Result<MotorDto>> Handle(GetMotorByIdQuery request, CancellationToken cancellationToken)
    {
        var motor = await _motorRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = MotorDtoMapper.Map(motor);

        return response;
    }
}
