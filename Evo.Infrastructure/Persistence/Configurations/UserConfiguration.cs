using Evo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Evo.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table & Key
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            // Id as GUID string (36 incl. hyphens). Adjust if you use different format.
            builder.Property(u => u.Id)
                   .HasMaxLength(36);

            // Email (unique, normalized to lowercase)
            var lowerCase = new ValueConverter<string, string>(
                v => v == null ? null! : v.ToLowerInvariant(),
                v => v
            );

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasConversion(lowerCase);

            builder.HasIndex(u => u.Email)
                   .IsUnique()
                   .HasDatabaseName("UX_Users_Email");

            // PasswordHash / PasswordSalt (binary with max lengths)
            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(512);

            builder.Property(u => u.PasswordSalt)
                   .IsRequired()
                   .HasMaxLength(128);

            // RolePermissions (enum as string for readability)
            builder.Property(u => u.RolePermissions)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(100);

            // Timestamps & flags
            builder.Property(u => u.CreatedAt)
                   .IsRequired();
            // Optionally also set a DB default (uncomment one of these per provider):
            // .HasDefaultValueSql("GETUTCDATE()");            // SQL Server
            // .HasDefaultValueSql("(now() at time zone 'utc')"); // PostgreSQL

            builder.Property(u => u.UpdatedAt);
            builder.Property(u => u.LastLogin);

            builder.Property(u => u.IsActive)
                   .HasDefaultValue(true);

            // Useful composite index for filtering by activity/role
            builder.HasIndex(u => new { u.IsActive, u.RolePermissions })
                   .HasDatabaseName("IX_Users_IsActive_Role");

            // --- Optional: One-to-one relations (uncomment & adjust if your related entities have FK: UserId) ---
            // builder.HasOne(u => u.Customer)
            //        .WithOne(c => c.User)
            //        .HasForeignKey<Customer>(c => c.UserId)
            //        .OnDelete(DeleteBehavior.Restrict);

            // builder.HasOne(u => u.Staff)
            //        .WithOne(s => s.User)
            //        .HasForeignKey<Staff>(s => s.UserId)
            //        .OnDelete(DeleteBehavior.Restrict);

            // --- Optional: SQL Server check constraints for binary lengths ---
            // builder.ToTable(t =>
            // {
            //     t.HasCheckConstraint("CK_Users_PasswordSalt_Length", "DATALENGTH([PasswordSalt]) BETWEEN 16 AND 128");
            //     t.HasCheckConstraint("CK_Users_PasswordHash_Length", "DATALENGTH([PasswordHash]) BETWEEN 20 AND 512");
            // });
        }
    }
}
