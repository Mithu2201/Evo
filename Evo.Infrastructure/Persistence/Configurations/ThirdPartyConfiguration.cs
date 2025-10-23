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
    public class ThirdPartyConfiguration : IEntityTypeConfiguration<ThirdParty>
    {
        public void Configure(EntityTypeBuilder<ThirdParty> builder)
        {
            builder.ToTable("ThirdParties");
            builder.HasKey(t => t.ThirdPartyId);

            builder.Property(t => t.CompanyName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(t => t.ContactName)
                   .HasMaxLength(150);

            builder.Property(t => t.Email)
                   .HasMaxLength(100);

            builder.Property(t => t.Phone)
                   .HasMaxLength(20);

            builder.Property(t => t.VerificationStatus)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.Email);
            builder.HasIndex(t => t.Phone);
            builder.HasIndex(t => new { t.CompanyName, t.Email });
        }
    }
}
