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

        public string UserId { get; set; } = "0";
        public AccountStatus? Status { get; set; } = AccountStatus.Active;
        public AdminPosition Position { get; set; } = AdminPosition.JuniorAdmin;

     
    }
}
