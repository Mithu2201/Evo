using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Queries.GetById
{
    public record GetThirdPartyDriverByIdQuery(Guid DriverId) : IRequest<ThirdPartyDriverDto?>;
}
