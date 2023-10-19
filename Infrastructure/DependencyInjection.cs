using Application.Abstractions;
using Domain.Abstractions;
using Infrastructure.InfrastructureLayer.EmailService;
using Infrastructure.Persistence.DataAccess;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories;
using MecalcEmailService;
using MecalcEmailService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
        options.UseNpgsql("Server=localhost;Port=5433;Database=vtol_design_db;User Id=jaco;Password=eVTOL2023;")
        );

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IConceptualDesignRepository, ConceptualDesignRepository>();
        services.AddTransient<IMotorRepository, MotorRepository>();
        services.AddTransient<IInvererRepository, InvererRepository>();
        services.AddTransient<IFuselageRepository, FuselageRepository>();
        services.AddTransient<IBladeRepository, BladeRepository>();
        services.AddTransient<IMissionParameterRepository, MissionParameterRepository>();
        services.AddTransient<IBatteryCellRepository, BatteryCellRepository>();
        services.AddTransient<IBatteryModuleRepository, BatteryModuleRepository>();
        services.AddTransient<IBatteryPackRepository, BatteryPackRepository>();
        services.AddTransient<IElectricVtolRepository, ElectricVtolRepository>();
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IAddressRespository, AddressRespository>();

        services.AddTransient<IEmailService, SmtpEmailService>();
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}
