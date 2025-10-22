using Evo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Evo.Domain.Entities
{
    public class Customer
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; } = default!;

        [Required, StringLength(150, MinimumLength = 2)]
        // Allow letters, marks, space, apostrophe, dot, hyphen (broad Unicode name support)
        [RegularExpression(@"^[\p{L}\p{M}][\p{L}\p{M}\.'\- ]{1,149}$",
            ErrorMessage = "Name can contain letters, spaces, apostrophes, dots and hyphens.")]
        public string Name { get; set; } = default!;

        [Required, StringLength(20, MinimumLength = 7)]
        // Prefer E.164 (e.g., +9477xxxxxxx). Loosen if you support local formatting.
        [RegularExpression(@"^\+?[1-9]\d{7,14}$",
            ErrorMessage = "Phone must be in international format (E.164), e.g. +9477xxxxxxx.")]
        public string Phone { get; set; } = default!;

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = default!;

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

        [Required]
        public AccountStatus Status { get; set; } = AccountStatus.Active;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        // Verification flags (useful for OTP / KYC flows)
        public bool? IsEmailVerified { get; set; }
        public bool? IsPhoneVerified { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation
        //public virtual ICollection<EventCart> EventCarts { get; set; } = new List<EventCart>();
        //ublic virtual ICollection<CustomRequest> CustomRequests { get; set; } = new List<CustomRequest>();


    }
}
