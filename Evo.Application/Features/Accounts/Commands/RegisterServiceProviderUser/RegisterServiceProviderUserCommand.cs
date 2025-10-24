using Evo.Application.Features.Accounts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterServiceProviderUser
{
    public class RegisterServiceProviderUserCommand
    {
        public required RegisterServiceProviderUserDto RegisterServiceProviderUserDto { get; set; }
    }
}
