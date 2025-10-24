using Evo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Domain.Entities
{
    public class ServiceProvider
    {
        [Key]
        public Guid ServiceProviderId { get; set; }

        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Required, MaxLength(200)]
        public string CompanyName { get; set; } = null!;

        [Required, MaxLength(200)]
        public string BrandName { get; set; } = null!;

        [Required, MaxLength(15), Phone]
        public string Phone { get; set; } = null!;

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; } = null!;

        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;

        // Address (flat). Consider an owned type for cleanliness.
        [StringLength(200)] public string? AddressLine1 { get; set; }
        [StringLength(200)] public string? AddressLine2 { get; set; }
        [StringLength(100)] public string? City { get; set; }
        [StringLength(100)] public string? District { get; set; }
        [StringLength(20)] public string? PostalCode { get; set; }
        [StringLength(100)] public string? Country { get; set; }

        public int MaxConcurrentBookings { get; set; }
        public int MinLeadTimeDays { get; set; } = 3;
        public int BookingWindowDays { get; set; } = 90;

        public int CreditPeriod { get; set; } = 90;

        [MaxLength(100)]
        public string? BusinessLicense { get; set; }

        [MaxLength(50)]
        public string? TaxId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Consider mapping to a JSON column or a typed owned entity
        public string CancellationPolicy { get; set; } = "{}";
        public string PaymentMethods { get; set; } = "[]";



        // Navigation
        //public virtual ICollection<ServiceProviderServiceCategory> ServiceProviderCategories { get; set; } = new HashSet<ServiceProviderServiceCategory>();
        //public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();
        //public virtual ICollection<Quotation> Quotations { get; set; } = new HashSet<Quotation>();
        //public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
