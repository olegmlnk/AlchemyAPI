using Alchemy.Infrastructure.Configurations;
using Alchemy.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure
{
    public class AlchemyDbContext : IdentityDbContext<UserEntity, IdentityRole<long>, long>
    {
        public AlchemyDbContext(DbContextOptions<AlchemyDbContext> options) : base(options) 
        {
            
        }

        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<MasterEntity> Masters { get; set; }
        public DbSet<MasterScheduleEntity> MasterSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new MasterConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
