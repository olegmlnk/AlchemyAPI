﻿using Alchemy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.MasterId).IsRequired();
            builder.Property(a => a.ServiceId).IsRequired();
            builder.Property(a => a.ScheduleSlotId).IsRequired();

            builder.HasOne(a => a.Master)
                .WithMany(m => m.Appointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Master)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.MasterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ScheduleSlot)
                .WithOne(ms => ms.Appointment)
                .HasForeignKey<Appointment>(a => a.ScheduleSlotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.ScheduleSlotId).IsUnique();
        }
    }
}