using Evo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Dtos
{
    // Request payload to create a User + Customer together.
    //Validation is handled by FluentValidation.
    public class RegisterCustomerUserDto
    {
        // ----- User -----
        public string Email { get; set; } = string.Empty;   // User.Email
        public string Password { get; set; } = string.Empty;
        //public string ConfirmPassword { get; set; } = string.Empty;

        //Optional: if not supplied, your handler can default to UserRole.Customer.
        public UserRole? RolePermissions { get; set; }= UserRole.Customer;

        public bool? IsActive { get; set; }                 // default true if null

        // ----- Customer -----
        public string CustomerName { get; set; } = string.Empty;  // Customer.Name
        public string CustomerPhone { get; set; } = string.Empty; // Customer.Phone
        public string? CustomerEmail { get; set; } = string.Empty; // Customer.Email (can mirror User.Email or be distinct)

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        //Optional: defaults to Active if null.
        public AccountStatus? Status { get; set; }
    }
}
