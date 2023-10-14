using Domain.Entities.ConceptDesign;
using Domain.Entities.DetailedDesign;
using Domain.Entities.DetailedDesign.Battery;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        internal DbSet<ConceptualVtolDesign> ConceptualDesign { get; set; }
        internal DbSet<ElectricVtolDesign> ElectricVtolDesigns { get; set; }
        internal DbSet<BatteryPack> BatteryPacks { get; set; }
        internal DbSet<BatteryModule> BatteryModules { get; set; }
        internal DbSet<Cell> Cells { get; set; }
        internal DbSet<Blade> Blades { get; set; }
        internal DbSet<Fuselage> Fuselages { get; set; }
        internal DbSet<Inverter> Inverters { get; set; }
        internal DbSet<Motor> Motors { get; set; }
        internal DbSet<MissionParameterEstimates> MissionParameters {  get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
