using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Update
{
    public record UpdateThirdPartyDriverCommand(Guid DriverId, UpdateThirdPartyDriverDto Changes) : IRequest<bool>;
}
