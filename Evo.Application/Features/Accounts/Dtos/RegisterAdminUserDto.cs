using Evo.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evo.Application.DTOs
{
    public class RegisterAdminUserDto
    {
         
        // ---- User Info ----
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        // ---- Admin Info ----
        [Required, MaxLength(150)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(15)]
        public string Phone { get; set; } = default!;

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
        public AdminPosition Position { get; set; } = AdminPosition.JuniorAdmin;

        [MaxLength(100)]
        public string? Department { get; set; }

        // Optional custom fields
        public string? Permissions { get; set; }

        // ---- Metadata ----
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
    }
}
