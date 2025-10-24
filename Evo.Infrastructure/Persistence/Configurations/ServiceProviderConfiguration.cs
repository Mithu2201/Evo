using Evo.Domain.Entities;
using Evo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evo.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// EF Core configuration for <see cref="ServiceProvider"/>.
    /// Notes:
    /// - Assumes SQL Server (GETUTCDATE(), []-quoted identifiers, check constraints syntax).
    ///   If you use PostgreSQL/MySQL, swap default SQL and check-constraint syntax accordingly.
    /// - Sets unique indexes on Email, Phone and UserId (1:1 user-to-service-provider profile).
    /// - Keeps enum as int; change to string conversion if preferred.
    /// </summary>
    public class ServiceProviderConfiguration : IEntityTypeConfiguration<ServiceProvider>
    {
        public void Configure(EntityTypeBuilder<ServiceProvider> builder)
        {
            builder.ToTable("ServiceProviders");

            // Primary Key
            builder.HasKey(sp => sp.Id);
            builder.Property(sp => sp.Id)
                   .ValueGeneratedNever(); // Guid generated client-side

            // Relationships
            builder.HasOne(sp => sp.User)
                   .WithMany() // If User has navigation: .WithOne(u => u.ServiceProvider)
                   .HasForeignKey(sp => sp.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes / Uniqueness
            builder.HasIndex(sp => sp.UserId).IsUnique(); // one ServiceProvider per User
            builder.HasIndex(sp => sp.Email).IsUnique();
            builder.HasIndex(sp => sp.Phone).IsUnique();

            // Required & Lengths
            builder.Property(sp => sp.CompanyName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(sp => sp.BrandName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(sp => sp.Phone)
                   .IsRequired()
                   .HasMaxLength(15)
                   .IsUnicode(false);

            builder.Property(sp => sp.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(sp => sp.Description)
                   .HasMaxLength(2000);

            // Address (flat)
            builder.Property(sp => sp.AddressLine1).HasMaxLength(200);
            builder.Property(sp => sp.AddressLine2).HasMaxLength(200);
            builder.Property(sp => sp.City).HasMaxLength(100);
            builder.Property(sp => sp.District).HasMaxLength(100);
            builder.Property(sp => sp.PostalCode).HasMaxLength(20);
            builder.Property(sp => sp.Country).HasMaxLength(100);

            // Numbers & Defaults
            builder.Property(sp => sp.MaxConcurrentBookings)
                   .IsRequired();

            builder.Property(sp => sp.MinLeadTimeDays)
                   .HasDefaultValue(3);

            builder.Property(sp => sp.BookingWindowDays)
                   .HasDefaultValue(90);

            builder.Property(sp => sp.CreditPeriod)
                   .HasDefaultValue(90);

            builder.Property(sp => sp.BusinessLicense)
                   .HasMaxLength(100);

            builder.Property(sp => sp.TaxId)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Flags & Enums
            builder.Property(sp => sp.IsEmailVerified)
                   .HasDefaultValue(false);

            builder.Property(sp => sp.IsPhoneVerified)
                   .HasDefaultValue(false);

            builder.Property(sp => sp.VerificationStatus)
                   .HasConversion<int>()
                   .HasDefaultValue(VerificationStatus.Pending);

            // Dates
            builder.Property(sp => sp.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(sp => sp.UpdatedAt)
                   .IsRequired(false);

            // JSON-like strings (consider real JSON column/owned type later)
            builder.Property(sp => sp.CancellationPolicy)
                   .HasColumnType("nvarchar(max)")
                   .HasDefaultValue("{}");

            builder.Property(sp => sp.PaymentMethods)
                   .HasColumnType("nvarchar(max)")
                   .HasDefaultValue("[]");

            // Check constraints (SQL Server syntax)
            builder.HasCheckConstraint("CK_ServiceProvider_MinLeadTimeDays", "[MinLeadTimeDays] >= 0");
            builder.HasCheckConstraint("CK_ServiceProvider_BookingWindowDays", "[BookingWindowDays] >= 0");
            builder.HasCheckConstraint("CK_ServiceProvider_CreditPeriod", "[CreditPeriod] >= 0");
            builder.HasCheckConstraint("CK_ServiceProvider_MaxConcurrentBookings", "[MaxConcurrentBookings] >= 0");
        }
    }
}

// In your DbContext:
// protected override void OnModelCreating(ModelBuilder modelBuilder)
// {
//     modelBuilder.ApplyConfiguration(new ServiceProviderConfiguration());
// }
