using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Delete
{
 public record DeleteThirdPartyDriverCommand(Guid DriverId) : IRequest<bool>;
}
