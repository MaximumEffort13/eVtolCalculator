using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
        options.UseNpgsql("Server=localhost;Port=5433;Database=vtol_design_db;User Id=jaco;Password=eVTOL2023;"));
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

        return services;
    }
}
