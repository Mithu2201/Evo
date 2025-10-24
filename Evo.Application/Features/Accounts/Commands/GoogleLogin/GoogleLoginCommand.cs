using Evo.Application.Features.Accounts.Dtos;
using MediatR;

namespace Evo.Application.Features.Accounts.Commands.GoogleLogin
{
    public class GoogleLoginCommand : IRequest<UserDto>
    {
        public required string IdToken { get; set; }
    }
}

