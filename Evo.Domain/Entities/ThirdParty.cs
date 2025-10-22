using Evo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Domain.Entities
{
    public class ThirdParty
    {
        [Key]
        public Guid ThirdPartyId { get; set; } = Guid.NewGuid();

        // Optional: if a vendor can log in
        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        [Required, StringLength(200)]
        public string CompanyName { get; set; } = default!;

        [StringLength(150)]
        public string? ContactName { get; set; }

        [StringLength(100), EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(200)] public string? AddressLine1 { get; set; }
        [StringLength(200)] public string? AddressLine2 { get; set; }
        [StringLength(100)] public string? City { get; set; }
        [StringLength(100)] public string? District { get; set; }
        [StringLength(20)] public string? PostalCode { get; set; }
        [StringLength(100)] public string? Country { get; set; }

        public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public virtual ICollection<ThirdPartyDriver> Drivers { get; set; } = new HashSet<ThirdPartyDriver>();
    }
}
