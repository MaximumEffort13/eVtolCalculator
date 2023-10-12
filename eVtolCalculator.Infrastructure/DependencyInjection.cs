using Application.Abstractions;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("Server=localhost;Port=5433;Database=vtol_design_db;User Id=jaco;Password=eVTOL2023;"));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IConceptualDesignRepository, ConceptualDesignRepository>();

        return services;
    }
}
