using Evo.Domain.Entities;
using Evo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evo.Infrastructure.Persistence.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {

        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            // Key
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .HasMaxLength(36)          // "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
                   .ValueGeneratedNever();

            // FKs
            builder.Property(a => a.UserId)
                   .IsRequired()
                   .HasMaxLength(36);

            builder.Property(a => a.StaffId)
                   .IsRequired()
                   .HasMaxLength(36);

            // Enums (store as int) + defaults
            builder.Property(a => a.Status)
                   .HasConversion<int>()
                   .HasDefaultValue(AccountStatus.Active);

            builder.Property(a => a.Position)
                   .HasConversion<int>()
                   .HasDefaultValue(AdminPosition.JuniorAdmin);

            // ---------- Relationships ----------

            // 1) Admin <-> User : One-to-one with explicit inverse to avoid shadow FK (UserId1)
            builder.HasOne(a => a.User)
                   .WithOne(u => u.Admin)                          // IMPORTANT: tie to the correct inverse
                   .HasForeignKey<Admin>(a => a.UserId)
                   .HasPrincipalKey<User>(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            // Make it truly 1:1 at the DB level
            builder.HasIndex(a => a.UserId).IsUnique();

            // 2) Admin <-> Staff : One-to-one (no inverse navigation on Staff)
            builder.HasOne(a => a.Staff)
                   .WithOne()                                      // no Staff.Admin property
                   .HasForeignKey<Admin>(a => a.StaffId)
                   .HasPrincipalKey<Staff>(s => s.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => a.StaffId).IsUnique();
        }
    }
}
