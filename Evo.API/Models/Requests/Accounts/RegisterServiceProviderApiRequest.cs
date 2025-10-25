using Evo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Evo.Application.Features.Accounts.Dtos;

namespace Evo.Application.Features.Accounts.Requests
{
    /// <summary>
    /// Request payload for registering a new Service Provider user via API.
    /// </summary>
    public class RegisterServiceProviderApiRequest
    {
        // ----- Account section -----

        /// <summary>User login email.</summary>
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>Raw password (will be hashed before storing).</summary>
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        /// <summary>Role to assign (defaults to ServiceProvider if null).</summary>
        public UserRole? RolePermissions { get; set; } = UserRole.ServiceProvider;

        /// <summary>Indicates if the user is active. Defaults to true if null.</summary>
        public bool? IsActive { get; set; } = true;

        // ----- ServiceProvider section -----

        [Required, StringLength(200)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string BrandName { get; set; } = string.Empty;

        /// <summary>E.164 phone format, e.g. +9477xxxxxxx</summary>
        [Required, StringLength(20, MinimumLength = 7)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        // Address (flat)
        [StringLength(200)] public string? AddressLine1 { get; set; }
        [StringLength(200)] public string? AddressLine2 { get; set; }
        [StringLength(100)] public string? City { get; set; }
        [StringLength(100)] public string? District { get; set; }
        [StringLength(20)] public string? PostalCode { get; set; }
        [StringLength(100)] public string? Country { get; set; }

        // Operational settings
        public int? MaxConcurrentBookings { get; set; } = 0;
        public int? MinLeadTimeDays { get; set; } = 3;
        public int? BookingWindowDays { get; set; } = 90;
        public int? CreditPeriod { get; set; } = 90;

        [StringLength(100)]
        public string? BusinessLicense { get; set; }

        [StringLength(50)]
        public string? TaxId { get; set; }

        /// <summary>Optional JSON strings (can later be mapped to typed models).</summary>
        public string? CancellationPolicy { get; set; } = "{}";
        public string? PaymentMethods { get; set; } = "[]";

        /// <summary>
        
    }
}
