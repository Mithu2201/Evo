using Evo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Evo.Application.DTOs
{
    public class RegisterAdminUserDto
    {

        [Required]
        public string StaffId { get; set; } = default!;

        public AccountStatus Status { get; set; } = AccountStatus.Active;
        public AdminPosition Position { get; set; } 
    }
}
