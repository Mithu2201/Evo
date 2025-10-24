 using Evo.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evo.Application.DTOs
{
    public class RegisterStaffUserDto
    {
        // ---- USER INFO ----
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [MaxLength(100)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        // ---- STAFF INFO ----
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = default!;

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^\+?[1-9]\d{7,14}$",
            ErrorMessage = "Phone must be in international format (E.164), e.g. +9477xxxxxxx.")]
        public string Phone { get; set; } = default!;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string StaffEmail { get; set; } = default!; // staff’s work email, different from login email if needed

        [MaxLength(200)]
        public string? AddressLine1 { get; set; }

        [MaxLength(200)]
        public string? AddressLine2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? District { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [Required]
        public StaffPosition Position { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        public string? Permissions { get; set; } // optional roles or access levels

        // ---- STATUS & METADATA ----
        public AccountStatus Status { get; set; } = AccountStatus.Active;

        public DateTime HireDate { get; set; } = DateTime.UtcNow;
    }
}
