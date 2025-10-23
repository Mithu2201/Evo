using Evo.Domain.Enums;

namespace Evo.API.Models.Requests.Accounts
{
    public class RegisterAdminApiRequest
    {
        public string Email { get; set; } = string.Empty;

        // Raw password (will be hashed before storing).
        public string Password { get; set; } = string.Empty;

        // Force admin role by default.
    

        // Indicates if the user is active. Defaults to true if null.
       

        // Staff’s phone number (E.164).
        

        // Staff’s email (can mirror user email).
        public string? StaffId { get; set; } = string.Empty;

  
        // Job
 


        // ----- Admin section -----
        // Status for the Admin row (defaults to Active).
        public AccountStatus? Status { get; set; } = AccountStatus.Active;

        // Admin role/grade inside Admin table.
        public AdminPosition AdminPosition { get; set; } = AdminPosition.JuniorAdmin;
    }
}
