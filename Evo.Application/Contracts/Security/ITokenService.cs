using Evo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Security
{
    public interface ITokenService
    {
        string CreateToken(User user, string role);
    }
}
