using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Dtos
{
    public class CustomerUserRegisteredDto
    {
        public string UserId { get; set; } // Required for linking to User

        public string Name { get; init; } = default!;

        public string Phone { get; init; } = default!;

        public string Email { get; set; } = default!;

        public string? AddressLine1 { get; init; }

        public string? AddressLine2 { get; init; }

        public string? City { get; init; }

        public string? District { get; init; }

        public string? PostalCode { get; init; }

        public string? Country { get; init; }

        public DateTime? DateOfBirth { get; init; }

        public bool? IsEmailVerified { get; init; }

        public bool? IsPhoneVerified { get; init; }
    }
}
