using Evo.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterCustomerUser
{
    public class RegisterCustomerUserCommand : IRequest<UserDto>
    {
        public required RegisterCustomerUserDto RegisterCustomerUserDto { get; set; }
    }
}
