using Evo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evo.Domain.Entities
{

    public class ThirdPartyDriver
    {
        [Key]
        public Guid DriverId { get; set; } = Guid.NewGuid();

        // Optional: link to app User (if driver logs in)
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        // Names
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = default!;
        [MaxLength(100)]
        public string? LastName { get; set; }

        // Contact
        [Required, MaxLength(200), EmailAddress]
        public string WorkEmail { get; set; } = default!;
        [Required, MaxLength(20)]
        public string PhoneE164 { get; set; } = default!; // +94...

        // Profile / KYC
        [MaxLength(20)]
        public string? NIC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }

        [StringLength(200)] public string? AddressLine1 { get; set; }
        [StringLength(200)] public string? AddressLine2 { get; set; }
        [StringLength(100)] public string? City { get; set; }
        [StringLength(100)] public string? District { get; set; }
        [StringLength(20)] public string? PostalCode { get; set; }
        [StringLength(100)] public string? Country { get; set; }

        // Driver & Vehicle details
        [MaxLength(50)]
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseExpiryUtc { get; set; }
        [MaxLength(30)]
        public string? VehicleType { get; set; } // Van, Lorry, Car, Bike ...
        [MaxLength(30)]
        public string? VehiclePlateNo { get; set; }

        // State
        public DriverStatus Status { get; set; } = DriverStatus.Pending;
        public DriverVerificationStatus VerificationStatus { get; set; } = DriverVerificationStatus.Pending;
        [Range(0, 5)]
        public decimal? Rating { get; set; }

        // Availability flags (basic)
        public bool IsAvailable { get; set; } = true;

        // Audit & soft delete
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Concurrency
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
