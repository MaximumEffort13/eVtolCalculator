using Application.Abstractions;
using Application.BatteryFacilitators.Commands;
using Application.BladeFacilitators.Commands;
using Application.DTO;
using Application.FuselageFacilitators.Commands;
using Application.InverterFacilitators.Commands;
using Application.MissionParameterFacilitators.Commands;
using Application.MotorFacilitators.Commands;

namespace Application.DetailDesignFacilitators.Commands;

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