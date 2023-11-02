using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Motors;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Motors;

internal sealed class GetAllMotorsQueryHandler : IQueryHandler<GetAllMotorsQuery, IEnumerable<MotorDto>>
{
    private readonly IMotorRepository _motorRepository;

    public GetAllMotorsQueryHandler(IMotorRepository motorRepository)
    {
        _motorRepository = motorRepository;
    }
    public async Task<Result<IEnumerable<MotorDto>>> Handle(GetAllMotorsQuery request, CancellationToken cancellationToken)
    {
        var motors = await _motorRepository.GetAllAsync(cancellationToken);

        List<MotorDto> motorDtos = new List<MotorDto>();

        motors.ForEach(motor =>
        {
            motorDtos.Add(MotorDtoMapper.Map(motor));
        });

        return motorDtos;
    }
}
