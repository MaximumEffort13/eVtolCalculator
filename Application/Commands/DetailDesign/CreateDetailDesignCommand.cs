using Application.Abstractions;
using Application.Commands.Battery;
using Application.Commands.Blade;
using Application.Commands.Fuselage;
using Application.Commands.Inverter;
using Application.Commands.Mission;
using Application.Commands.Motors;
using Application.DTO;

namespace Application.Commands.DetailDesign;

public sealed record CreateDetailedDesignCommand(string Name,
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
    CreateBatteryPackCommand Battery,
    CreateMotorCommand Motor,
    CreateInverterCommand Inverter,
    CreateFuselageCommand Fuselage,
    CreateBladeCommand Blade,
    CreateMissionParameterCommand Mission,
    int MotorQuantity,
    int BladePerMotorCount);