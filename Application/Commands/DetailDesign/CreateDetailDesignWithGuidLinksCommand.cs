using Application.Abstractions;
using Application.Commands.Fuselage;
using Application.Commands.Mission;
using Application.DTO;

namespace Application.Commands.DetailDesign;

public sealed record CreateDetailDesignWithGuidLinksCommand(Guid UserId,
    string Name,
    Guid BatteryId,
    Guid MotorId,
    Guid InverterId,
    Guid BladeId,
    Guid MissionId,
    Guid FuselageId,
    int MotorQuantity,
    int BladePerMotorQuantity
    ) : ICommand<ElectricVtolDesignDto>;

public sealed record CreateDetailDesignGuidLinkInput(
    string Name,
    string BatteryPackId,
    string MotorId,
    string InverterId,
    string BladeId,
    MissionParameterInsert MissionParameters,
    FuselageInsert Fuselage,
    int MotorQuantity,
    int BladePerMotorQuantity
    );