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

        [Required, ForeignKey(nameof(Staff))]
        public string StaffId { get; set; } = default!;

        public virtual Staff Staff { get; set; } = default!;

        public AccountStatus Status { get; set; } = AccountStatus.Active;

        [Required]
        public AdminPosition Position { get; set; } 


    }
}