using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class MasterConfiguration : IEntityTypeConfiguration<MasterEntity>
    {
        public void Configure(EntityTypeBuilder<MasterEntity> builder)
        {
            builder.ToTable("Masters");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(Master.MAX_NAME_LENGTH)
                .IsRequired();
            builder.Property(m => m.Description)
                .HasMaxLength(Master.MAX_DESCRIPTION_LENGTH)
                .IsRequired();
            builder.Property(m => m.Experience)
                .HasMaxLength(Master.MAX_EXPERIENCE_LENGTH)
                .IsRequired();

            builder.HasMany(m => m.Appointments)
                .WithOne(a => a.Master)
                .HasForeignKey(a => a.MasterId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(m => m.ScheduleSlots)
                .WithOne(ms => ms.Master)
                .HasForeignKey(ms => ms.MasterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
