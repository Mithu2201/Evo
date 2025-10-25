using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Create
{
    public record CreateThirdPartyDriverCommand(CreateThirdPartyDriverDto Driver) : IRequest<ThirdPartyDriverDto>;
}
