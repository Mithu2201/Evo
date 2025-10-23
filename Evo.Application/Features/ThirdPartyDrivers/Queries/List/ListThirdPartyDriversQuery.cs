using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using Evo.Domain.Enums;
using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Queries.List
{
    public record ListThirdPartyDriversQuery(
        DriverStatus? Status,
        DriverVerificationStatus? VerificationStatus,
        bool? IsAvailable) : IRequest<IReadOnlyList<ThirdPartyDriverDto>>;
}
