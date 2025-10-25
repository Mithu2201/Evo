using Evo.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        public required LoginDto LoginDto { get; set; }
    }
}
