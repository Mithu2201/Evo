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
                   .IsRequired()
                   .HasMaxLength(36)           // "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
                   .ValueGeneratedNever();     // value set in the entity (Guid.NewGuid().ToString())

            // Required FK to Staff
            builder.Property(a => a.StaffId)
                   .IsRequired()
                   .HasMaxLength(36);

            // Enums (store as int) + defaults
            builder.Property(a => a.Status)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasDefaultValue(AccountStatus.Active);

            builder.Property(a => a.Position)
                   .IsRequired()
                   .HasConversion<int>();
                   
            // ---------- Relationships ----------
            // Admin <-> Staff : 1:1 (no inverse nav on Staff)
            builder.HasOne(a => a.Staff)
                   .WithOne()
                   .HasForeignKey<Admin>(a => a.StaffId)
                   .HasPrincipalKey<Staff>(s => s.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            // Ensure one Admin per Staff
            builder.HasIndex(a => a.StaffId).IsUnique();
        }
    }
}
