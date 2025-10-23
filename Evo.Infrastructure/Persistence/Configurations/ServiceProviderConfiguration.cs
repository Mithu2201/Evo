using Evo.Domain.Entities;
using Evo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Infrastructure.Persistence.Configurations
{
    public class ServiceProviderConfiguration : IEntityTypeConfiguration<ServiceProvider>
    {
        public void Configure(EntityTypeBuilder<ServiceProvider> builder)
        {
            // Table
            builder.ToTable("ServiceProviders");

            // Primary key
            builder.HasKey(sp => sp.ServiceProviderId);

            // Relationships
            builder.HasOne(sp => sp.User)
                   .WithOne(u => u.ServiceProvider) // assuming User has navigation property 'ServiceProvider'
                   .HasForeignKey<ServiceProvider>(sp => sp.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(sp => sp.ServiceProviderId)
                   .IsRequired();

            builder.Property(sp => sp.UserId)
                   .IsRequired();

            builder.Property(sp => sp.CompanyName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(sp => sp.BrandName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(sp => sp.Phone)
                   .HasMaxLength(15)
                   .IsRequired();

            builder.Property(sp => sp.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(sp => sp.IsEmailVerified)
                   .HasDefaultValue(false);

            builder.Property(sp => sp.IsPhoneVerified)
                   .HasDefaultValue(false);

            builder.Property(sp => sp.Description)
                   .HasMaxLength(2000);

            builder.Property(sp => sp.VerificationStatus)
                   .HasConversion<string>() // Store enum as string for readability
                   .HasMaxLength(20)
                   .HasDefaultValue(VerificationStatus.Pending)
                   .IsRequired();

            builder.Property(sp => sp.AddressLine1)
                   .HasMaxLength(200);

            builder.Property(sp => sp.AddressLine2)
                   .HasMaxLength(200);

            builder.Property(sp => sp.City)
                   .HasMaxLength(100);

            builder.Property(sp => sp.District)
                   .HasMaxLength(100);

            builder.Property(sp => sp.PostalCode)
                   .HasMaxLength(20);

            builder.Property(sp => sp.Country)
                   .HasMaxLength(100);

            builder.Property(sp => sp.MaxConcurrentBookings)
                   .HasDefaultValue(0);

            builder.Property(sp => sp.MinLeadTimeDays)
                   .HasDefaultValue(3);

            builder.Property(sp => sp.BookingWindowDays)
                   .HasDefaultValue(90);

            builder.Property(sp => sp.CreditPeriod)
                   .HasDefaultValue(90);

            builder.Property(sp => sp.BusinessLicense)
                   .HasMaxLength(100);

            builder.Property(sp => sp.TaxId)
                   .HasMaxLength(50);

            builder.Property(sp => sp.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(sp => sp.UpdatedAt)
                   .IsRequired(false);

            builder.Property(sp => sp.CancellationPolicy)
                   .HasColumnType("nvarchar(max)")
                   .HasDefaultValue("{}");

            builder.Property(sp => sp.PaymentMethods)
                   .HasColumnType("nvarchar(max)")
                   .HasDefaultValue("[]");

            // Indexes
            builder.HasIndex(sp => sp.Email)
                   .IsUnique();

            builder.HasIndex(sp => sp.CompanyName);
            builder.HasIndex(sp => sp.BrandName);
        }
    }
}
