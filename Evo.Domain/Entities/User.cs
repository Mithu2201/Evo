using Evo.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evo.Domain.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "UserName must be a valid email address.")]
        public string Email { get; set; } = default!;

        // Store a hash, not plaintext (e.g., Argon2, BCrypt, PBKDF2 output)
        [Required, StringLength(512, MinimumLength = 20)]
        [DataType(DataType.Password)]
        public byte[] PasswordHash { get; set; } = default!; // The hashed password

        // Random salt used during hashing (16–128 bytes is secure)
        [Required]
        [MinLength(16, ErrorMessage = "PasswordSalt must be at least 16 bytes.")]
        [MaxLength(128, ErrorMessage = "PasswordSalt must be at most 128 bytes.")]
        public byte[] PasswordSalt { get; set; } = default!;


        [Required]
        public UserRole RolePermissions { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        [DataType(DataType.DateTime)]
        public DateTime? LastLogin { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
       
        public virtual Staff Staff { get; set; }
        //public virtual ServiceProvider? ServiceProvider { get; set; }
        //public virtual ThirdParty? ThirdParty { get; set; }
        //public virtual EventOrganiser? EventOrganiser { get; set; }

        //public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        //public virtual ICollection<Reminder> Reminders { get; set; } = new HashSet<Reminder>();
    }

}