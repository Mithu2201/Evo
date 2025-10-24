using Evo.Domain.Enums;

namespace Evo.API.Models.Requests.Accounts
{
    public class RegisterAdminApiRequest
    {
  


        // Force admin role by default.
    

        // Indicates if the user is active. Defaults to true if null.
       

        // Staff’s phone number (E.164).
        

        // Staff’s email (can mirror user email).
        public string StaffId { get; set; } = string.Empty;

        // ----- Admin section -----
        // Status for the Admin row (defaults to Active).
        public AccountStatus Status { get; set; } = AccountStatus.Active;

        // Admin role/grade inside Admin table.
        public AdminPosition AdminPosition { get; set; }
    }
}
