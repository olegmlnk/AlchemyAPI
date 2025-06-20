using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alchemy.Infrastructure.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<ServiceEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceEntity> builder)
        {
            builder.ToTable("Service");
            
            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.Title)
                .HasMaxLength(Service.MAX_TITLE_LENGTH)
                .IsRequired();

            builder.HasIndex(s => s.Title)
                .IsUnique();

            builder.Property(s => s.Description)
                .HasMaxLength(Service.MAX_DESCRIPTION_LENGTH)
                .IsRequired();

            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.Duration)
                .IsRequired();
        }
    }
}
