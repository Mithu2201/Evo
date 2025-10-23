using Evo.Domain.Entities;
using Evo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Persistence
{
    public interface IThirdPartyDriverRepository
    {
        Task AddAsync(ThirdPartyDriver entity, CancellationToken ct = default);
        Task<ThirdPartyDriver?> GetByIdAsync(Guid driverId, bool includeThirdParty = false, CancellationToken ct = default);
        Task UpdateAsync(ThirdPartyDriver entity, CancellationToken ct = default);
        Task SoftDeleteAsync(Guid driverId, CancellationToken ct = default);

        Task<IReadOnlyList<ThirdPartyDriver>> ListAsync(
            Guid? thirdPartyId = null,
            DriverStatus? status = null,
            DriverVerificationStatus? verification = null,
            bool? isAvailable = null,
            CancellationToken ct = default);
    }
}
