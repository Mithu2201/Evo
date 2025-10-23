using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using Evo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Create
{
    public record CreateThirdPartyDriverCommand(CreateThirdPartyDriverDto Driver) : IRequest<ThirdPartyDriverVm>;
}
