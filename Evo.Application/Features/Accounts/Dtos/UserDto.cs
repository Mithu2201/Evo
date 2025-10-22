namespace Evo.Application.Features.Accounts.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}