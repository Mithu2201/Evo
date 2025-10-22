using Evo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Domain.Entities
{
    public class Staff
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();   // AdminId -> StaffId

        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; } = default!;

        // ---- Names ----
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = default!;

        [MaxLength(100)]
        public string? LastName { get; set; }


        // ---- Contact ----
        [Required, MaxLength(20)]
        public string Phone { get; set; } = default!;     // E.164 (+94...) format பரிந்துரை

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = default!;     // unique (Index attribute மேலே) 
        // Address 

        [StringLength(200)]
        public string? AddressLine1 { get; set; }

        [StringLength(200)]
        public string? AddressLine2 { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? District { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }
        // ---- Job ----
         [Required]
        public StaffPosition Position { get; set; } = StaffPosition.Junior;

        [MaxLength(100)]
        public string? Department { get; set; }

        // ---- Dates ----
        public DateTime HireDate { get; set; }
        public DateTime? EndDate { get; set; }

        // ---- Status ----
        public AccountStatus Status { get; set; } = AccountStatus.Active;



        // ---- Permissions ----
        public string? Permissions { get; set; }

        // ---- Audit / Concurrency (nice-to-have) ----
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
