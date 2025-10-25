using Evo.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterServiceProviderUser
{
    public class RegisterServiceProviderUserCommand :IRequest<UserDto>
    {
        public required RegisterServiceProviderUserDto RegisterServiceProviderUserDto { get; set; }
    }
}
