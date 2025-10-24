using Evo.Domain.Entities;
using Evo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evo.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Table
            builder.ToTable("Customers");

            // Primary key
            builder.HasKey(c => c.Id);

            // Relationships
            builder.HasOne(c => c.User)
                   .WithOne(u => u.Customer)
                   .HasForeignKey<Customer>(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(c => c.Id)
                   .HasMaxLength(36)
                   .IsRequired();

            builder.Property(c => c.UserId)
                   .IsRequired();

            builder.Property(c => c.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(c => c.Phone)
                   .HasMaxLength(20)
                   .IsRequired(false);  // Explicitly optional

            builder.Property(c => c.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.AddressLine1)
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(c => c.AddressLine2)
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(c => c.City)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(c => c.District)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(c => c.PostalCode)
                   .HasMaxLength(20)
                   .IsRequired(false);

            builder.Property(c => c.Country)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(c => c.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired()
                   .HasDefaultValue(AccountStatus.Active);

            builder.Property(c => c.DateOfBirth)
                   .HasColumnType("date")
                   .IsRequired(false);

            builder.Property(c => c.IsEmailVerified)
                   .HasDefaultValue(false);

            builder.Property(c => c.IsPhoneVerified)
                   .HasDefaultValue(false);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(c => c.UpdatedAt)
                   .IsRequired(false);

            // Indexes
            builder.HasIndex(c => c.Email)
                   .IsUnique();
        }
    }
}
