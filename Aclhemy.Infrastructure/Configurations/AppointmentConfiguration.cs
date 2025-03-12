using Alchemy.Infrastructure.Entities;
using Alchemy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AppointmentDate)
                .IsRequired();


            builder.Property(a => a.Description)
                .HasMaxLength(Appointment.MAX_DESCRIPTION_LENGTH)
                .IsRequired();

            builder.Property(a => a.MasterId)
                .IsRequired();

            builder.Property(a => a.UserId)
                 .IsRequired();

            builder.HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Master)
                .WithMany(m => m.Appointments.Cast<AppointmentEntity>())
                .HasForeignKey(a => a.MasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => new { a.MasterId, a.AppointmentDate })
                .IsUnique();
        }

    }
}
