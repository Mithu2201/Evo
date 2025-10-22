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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table & key
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            // Id: string GUID (36 chars like "d3f9...-..."). Keep as NVARCHAR(36).
            builder.Property(u => u.Id)
                   .HasMaxLength(36)
                   .IsRequired();

            // Email (acts as username): required, unique (case-insensitive)
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   // For SQL Server; remove/adjust for other providers
                   .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            builder.HasIndex(u => u.Email)
                   .IsUnique()
                   .HasDatabaseName("UX_Users_Email");

            // Password hash/salt as VARBINARY with upper bounds
            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(512); // VARBINARY(512)

            builder.Property(u => u.PasswordSalt)
                   .IsRequired()
                   .HasMaxLength(128); // VARBINARY(128)

            // Enum as string (readable). Remove HasConversion<string>() to store as int.
            builder.Property(u => u.RolePermissions)
                   .HasConversion<string>()
                   .HasMaxLength(64)
                   .IsRequired();

            // Timestamps & flags
            builder.Property(u => u.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.UpdatedAt)
                   .IsRequired(false);

            builder.Property(u => u.LastLogin)
                   .IsRequired(false);

            builder.Property(u => u.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            // One-to-one with Customer (FK on Customer.UserId)
            builder.HasOne(u => u.Customer)
                   .WithOne(c => c.User!)
                   .HasForeignKey<Customer>(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // DB-level checks (SQL Server)
            builder.ToTable(t =>
            {
                // Length sanity on Id (36 with hyphens) — not a full GUID regex, just a safety check
                t.HasCheckConstraint("CK_Users_Id_Len", "LEN([Id]) = 36");
                // Enforce email length explicitly (redundant with column, but clear)
                t.HasCheckConstraint("CK_Users_Email_Len", "LEN([Email]) BETWEEN 5 AND 100");
                // Hash/salt byte ranges
                t.HasCheckConstraint("CK_Users_PasswordHash_Len", "DATALENGTH([PasswordHash]) BETWEEN 20 AND 512");
                t.HasCheckConstraint("CK_Users_PasswordSalt_Len", "DATALENGTH([PasswordSalt]) BETWEEN 16 AND 128");
            });
        }
    }
}
