﻿using Alchemy.Infrastructure.Entities;
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

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(a => a.ScheduleSlotId)
                .IsRequired();

            builder.Property(a => a.Description)
                .HasMaxLength(Appointment.MAX_DESCRIPTION_LENGTH)
                .IsRequired();

            builder.Property(a => a.MasterId)
                .IsRequired();

            builder.Property(a => a.UserId)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.ServiceId)
                .IsRequired();

            builder.HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(a => a.Master)
                .WithMany(m => m.Appointments)
                .HasForeignKey(a => a.MasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => new { a.MasterId, a.ScheduleSlotId })
                .IsUnique();
        }
    }
}
