using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure
{
    public class AlchemyDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AlchemyDbContext(DbContextOptions<AlchemyDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<MasterSchedule> MasterSchedules { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new MasterConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
