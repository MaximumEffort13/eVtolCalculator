using Application.Abstractions;
using Application.Commands.Battery;
using Application.Commands.Blade;
using Application.Commands.Fuselage;
using Application.Commands.Inverter;
using Application.Commands.Mission;
using Application.Commands.Motors;
using Application.DTO;

namespace Application.Commands.DetailDesign;

public sealed record CreateDetailedDesignCommand(Guid UserId,
        string Name,
        BatteryPackDto Battery,
        InverterDto Inverter,
        MotorDto Motor,
        BladeDto Blade,
        FuselageDto Fuselage,
        MissionParameterDto MissionParameter,
        int MotorQuantity,
        int BladePerMotorQuantity,
        double PayloadWeight,
        double FlightTimeInMinutes) : ICommand<ElectricVtolDesignDto>;

public sealed record InsertDetailDesignDto(
    string Name,
    BatteryPackInsert Battery,
    MotorInsert Motor,
    InverterInsert Inverter,
    FuselageInsert Fuselage,
    BladeInsert Blade,
    MissionParameterInsert Mission,
    int MotorQuantity,
    int BladePerMotorCount);
