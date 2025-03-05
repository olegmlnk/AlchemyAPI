using Alchemy.Infrastructure.Configurations;
using Alchemy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure
{
    public class AlchemyDbContext : DbContext
    {
        public AlchemyDbContext(DbContextOptions<AlchemyDbContext> options) : base(options) { }

        public DbSet<AppointmentEntity> Appointments { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Master> Masters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        }
    }
}
