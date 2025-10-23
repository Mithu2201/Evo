using Evo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Infrastructure.Persistence.Configurations
{
    public class ThirdPartyDriverConfiguration : IEntityTypeConfiguration<ThirdPartyDriver>
    {
        public void Configure(EntityTypeBuilder<ThirdPartyDriver> builder)
        {
            builder.ToTable("ThirdPartyDrivers");
            builder.HasKey(d => d.DriverId);

            builder.Property(d => d.WorkEmail)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(d => d.PhoneE164)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(d => d.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(d => d.LastName)
                   .HasMaxLength(100);

            builder.Property(d => d.NIC)
                   .HasMaxLength(20);

            builder.Property(d => d.PhotoUrl)
                   .HasMaxLength(500);

            builder.Property(d => d.LicenseNumber)
                   .HasMaxLength(50);

            builder.Property(d => d.VehicleType)
                   .HasMaxLength(30);

            builder.Property(d => d.VehiclePlateNo)
                   .HasMaxLength(30);

            builder.Property(d => d.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(d => d.VerificationStatus)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(d => d.Rating)
                   .HasPrecision(2, 1);

            builder.Property(d => d.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(d => d.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(d => d.RowVersion)
                   .IsRowVersion();

            builder.HasOne(d => d.ThirdParty)
                   .WithMany(t => t.Drivers)
                   .HasForeignKey(d => d.ThirdPartyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.User)
                   .WithMany()
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Soft-delete global filter
            builder.HasQueryFilter(d => !d.IsDeleted);

            // Useful indexes/uniques within a vendor
            builder.HasIndex(d => new { d.ThirdPartyId, d.WorkEmail }).IsUnique();
            builder.HasIndex(d => new { d.ThirdPartyId, d.PhoneE164 }).IsUnique();
            builder.HasIndex(d => d.LicenseNumber);
            builder.HasIndex(d => d.VehiclePlateNo);
            builder.HasIndex(d => new { d.ThirdPartyId, d.Status, d.IsAvailable });
        }
    }
}
