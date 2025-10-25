using Evo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evo.Infrastructure.Persistence.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            // ---- Table ----
            builder.ToTable("Staff");

            // ---- Primary Key ----
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasMaxLength(36)
                   .IsRequired()
                   .ValueGeneratedNever(); // You manually assign Guid.NewGuid().ToString()

            // ---- Relationships ----
            builder.Property(s => s.UserId)
                   .IsRequired()
                   .HasMaxLength(36);

            builder.HasOne(s => s.User)
                   .WithOne() // or .WithOne(u => u.Staff) if User has navigation
                   .HasForeignKey<Staff>(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ---- Names ----
            builder.Property(s => s.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.LastName)
                   .HasMaxLength(100);

            // ---- Contact ----
            builder.Property(s => s.Phone)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.HasIndex(s => s.Email)
                   .IsUnique()
                   .HasDatabaseName("UX_Staff_Email");

            // ---- Address ----
            builder.Property(s => s.AddressLine1).HasMaxLength(200);
            builder.Property(s => s.AddressLine2).HasMaxLength(200);
            builder.Property(s => s.City).HasMaxLength(100);
            builder.Property(s => s.District).HasMaxLength(100);
            builder.Property(s => s.PostalCode).HasMaxLength(20).IsUnicode(false);
            builder.Property(s => s.Country).HasMaxLength(100);

            // ---- Job ----
            builder.Property(s => s.Position)
                   .IsRequired()
                   .HasConversion<string>()  // stores enum as text ("Junior", "Senior", etc.)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(s => s.Department)
                   .HasMaxLength(100);

            // ---- Dates ----
            builder.Property(s => s.HireDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.EndDate);

            // ---- Status ----
            builder.Property(s => s.Status)
                   .IsRequired()
                   .HasConversion<string>()  // stores enum as text
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // ---- Permissions ----
            builder.Property(s => s.Permissions)
                   .HasColumnType("nvarchar(max)");

            // ---- Audit ----
            builder.Property(s => s.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.UpdatedAt);

            // ---- Constraints ----
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Staff_Phone_Length", "LEN([Phone]) BETWEEN 7 AND 20");
            });

            // ---- Admin relationship ----
            builder.HasOne(s => s.Admin)
                   .WithOne(a => a.Staff)
                   .HasForeignKey<Admin>(a => a.StaffId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(s => s.User)
                   .WithOne(u => u.Staff)
                   .HasForeignKey<Staff>(s => s.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
