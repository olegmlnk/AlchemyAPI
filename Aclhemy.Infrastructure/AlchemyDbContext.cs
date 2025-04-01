using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Configurations;
using Alchemy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure
{
    public class AlchemyDbContext : DbContext
    {
        public AlchemyDbContext(DbContextOptions<AlchemyDbContext> options) : base(options) { }

        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<MasterEntity> Masters { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MasterSchedule> MasterSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new MasterConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
