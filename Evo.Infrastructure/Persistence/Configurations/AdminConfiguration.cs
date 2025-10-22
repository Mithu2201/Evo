using Evo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evo.Infrastructure.Persistence.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            // ---- Table ----
            builder.ToTable("Admins");

            // ---- Primary Key ----
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .HasMaxLength(36)
                   .IsRequired()
                   .ValueGeneratedNever(); // since you're assigning Guid.NewGuid().ToString()

            // ---- Relationships ----
            builder.Property(a => a.UserId)
                   .IsRequired()
                   .HasMaxLength(36);

            builder.HasOne(a => a.User)
                   .WithOne() // or .WithOne(u => u.Admin) if User has navigation
                   .HasForeignKey<Admin>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ---- Basic Info ----
            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(a => a.Phone)
                   .IsRequired()
                   .HasMaxLength(15)
                   .IsUnicode(false);

            builder.Property(a => a.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // Unique Email per admin
            builder.HasIndex(a => a.Email)
                   .IsUnique()
                   .HasDatabaseName("UX_Admins_Email");

            // ---- Address ----
            builder.Property(a => a.AddressLine1).HasMaxLength(200);
            builder.Property(a => a.AddressLine2).HasMaxLength(200);
            builder.Property(a => a.City).HasMaxLength(100);
            builder.Property(a => a.District).HasMaxLength(100);
            builder.Property(a => a.PostalCode).HasMaxLength(20).IsUnicode(false);
            builder.Property(a => a.Country).HasMaxLength(100);

            // ---- Job / Work ----
            builder.Property(a => a.Position)
                   .IsRequired()
                   .HasConversion<string>()  // store enum as text ("JuniorAdmin", "SeniorAdmin", etc.)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(a => a.Department)
                   .HasMaxLength(100);

            builder.Property(a => a.HireDate)
                   .HasDefaultValueSql("GETUTCDATE()"); // SQL Server default UTC time

            // ---- Status ----
            builder.Property(a => a.Status)
                   .IsRequired()
                   .HasConversion<string>()  // store as readable text
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // ---- Permissions ----
            builder.Property(a => a.Permissions)
                   .HasColumnType("nvarchar(max)");

            // ---- Constraints ----
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Admins_Phone_Length", "LEN([Phone]) BETWEEN 7 AND 15");
            });
        }
    }
}
