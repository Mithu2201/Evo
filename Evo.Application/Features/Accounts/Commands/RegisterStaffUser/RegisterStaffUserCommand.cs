using Evo.Application.Features.Accounts.Dtos;
using MediatR;
using Evo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterStaffUser
{
    public class RegisterStaffUserCommand : IRequest<UserDto>
    {
        public required RegisterStaffUserDto RegisterStaffUserDto { get; set; }
    }
}
