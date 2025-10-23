using Evo.Application.DTOs;
using Evo.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterAdminUser
{
    public class RegisterAdminUserCommand : IRequest<UserDto>
    {
        public required RegisterAdminUserDto RegisterAdminUserDto { get; set; }
    
    }
}

   