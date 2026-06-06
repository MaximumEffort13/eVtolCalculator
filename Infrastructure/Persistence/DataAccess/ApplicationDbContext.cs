using Domain.Entities;
using Domain.Entities.AuthenticationModels;
using Domain.Entities.ConceptDesign;
using Domain.Entities.DetailedDesign;
using Domain.Entities.DetailedDesign.Battery;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataAccess
{
    public sealed class ApplicationDbContext : IdentityDbContext<IdentityUserExtender>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        internal DbSet<ConceptualVtolDesign> ConceptualDesign { get; set; }
        internal DbSet<ElectricVtolDesign> ElectricVtolDesigns { get; set; }
        internal DbSet<BatteryPack> BatteryPacks { get; set; }
        internal DbSet<BatteryModule> BatteryModules { get; set; }
        internal DbSet<Cell> Cells { get; set; }
        internal DbSet<BladeEntity> Blades { get; set; }
        internal DbSet<FuselageEntity> Fuselages { get; set; }
        internal DbSet<InverterEntity> Inverters { get; set; }
        internal DbSet<Motor> Motors { get; set; }
        internal DbSet<MissionParameterEstimates> MissionParameters { get; set; }
        internal DbSet<PersonEntity> People { get; set; }
        internal DbSet<AddressEntity> Addresses { get; set; }
        internal DbSet<DesignConstantsEntity> DesignConstants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
