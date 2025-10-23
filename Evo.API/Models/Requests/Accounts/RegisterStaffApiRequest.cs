using Evo.Domain.Enums;

namespace Evo.API.Models.Requests.Accounts
{
    /// <summary>
    /// API request payload for registering a User + Staff together.
    /// This is the shape received by the controller (API layer).
    /// Validation will be handled by FluentValidation.
    /// </summary>
    public class RegisterStaffApiRequest
    {
        // ----- User section -----

        /// <summary>
        /// User's login email (used for authentication).
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Raw password (hashed before storing).
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Optional custom permission or role (defaults to Staff).
        /// </summary>
        public string? Permissions { get; set; }

        /// <summary>
        /// Optional account status (defaults to Active).
        /// </summary>
        public AccountStatus? Status { get; set; } = AccountStatus.Active;


        // ----- Staff section -----

        /// <summary>
        /// Staff member’s first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Staff member’s last name (optional).
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Staff’s phone number in E.164 format (e.g. +9477xxxxxxx).
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Staff’s work email (can differ from login email).
        /// </summary>
        public string StaffEmail { get; set; } = string.Empty;

        /// <summary>
        /// Address information.
        /// </summary>
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        /// <summary>
        /// Job-related details.
        /// </summary>
        public StaffPosition Position { get; set; } = StaffPosition.Junior;
        public string? Department { get; set; }

        /// <summary>
        /// Hire date (defaults to current date if not supplied).
        /// </summary>
        public DateTime? HireDate { get; set; } = DateTime.UtcNow;
    }
}
