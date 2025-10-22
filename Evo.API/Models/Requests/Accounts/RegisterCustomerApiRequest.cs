using Evo.Domain.Enums;

namespace Evo.API.Models.Requests.Accounts
{

    // API request payload for registering a User + Customer together.
    // This is the shape received by the controller (API layer).
    // Validation handled by FluentValidation.
    public class RegisterCustomerApiRequest
    {
        // ----- User section -----

        //User's login email.
        public string Email { get; set; } = string.Empty;

        //Raw password (will be hashed before storing).
        public string Password { get; set; } = string.Empty;

        //Role to assign (defaults to Customer if null).
        public UserRole? RolePermissions { get; set; } = UserRole.Customer;

        //Indicates if the user is active. Defaults to true if null.
        public bool? IsActive { get; set; }

        // ----- Customer section -----

        //Customer’s full name.
        public string CustomerName { get; set; } = string.Empty;

        //Customer’s phone number.
        public string CustomerPhone { get; set; } = string.Empty;

        //Customer’s email (can mirror user email).
        public string? CustomerEmail { get; set; } = string.Empty;

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        // Date of birth (optional).
        public DateTime? DateOfBirth { get; set; }

        // Optional account status (defaults to Active).
        public AccountStatus? Status { get; set; } = AccountStatus.Active;
    }
}
