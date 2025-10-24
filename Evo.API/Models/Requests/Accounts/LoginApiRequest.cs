namespace Evo.API.Models.Requests.Accounts
{
    public class LoginApiRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
