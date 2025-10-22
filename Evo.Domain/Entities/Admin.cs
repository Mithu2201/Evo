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
    public class Admin
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; } = default!;

        [Required, MaxLength(150)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(15)]
        public string Phone { get; set; } = default!;

        [Required, MaxLength(100), EmailAddress]
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
        public AccountStatus Status { get; set; } = AccountStatus.Active;

        [Required]
        public AdminPosition Position { get; set; } = AdminPosition.JuniorAdmin;

        [MaxLength(100)]
        public string? Department { get; set; }

        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        public string? Permissions { get; set; } // JSON/string of permissions

    }
}
