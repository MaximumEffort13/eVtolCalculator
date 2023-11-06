using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Motors;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Motors;

internal sealed class GetAllMotorsQueryHandler : IQueryHandler<GetAllMotorsQuery, List<MotorDto>>
{
    private readonly IMotorRepository _motorRepository;

    public GetAllMotorsQueryHandler(IMotorRepository motorRepository)
    {
        _motorRepository = motorRepository;
    }
    public async Task<Result<List<MotorDto>>> Handle(GetAllMotorsQuery request, CancellationToken cancellationToken)
    {
        var motors = await _motorRepository.GetAllAsync(request.UserId, cancellationToken);

        List<MotorDto> motorDtos = new ();

        motors.ForEach(motor =>
        {
            var mapped = MotorDtoMapper.Map(motor);
            motorDtos.Add(mapped);
        });

        return motorDtos;
    }
}
