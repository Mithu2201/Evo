using Evo.Application.Contracts.Persistence;
using Evo.Domain.Entities;
using Evo.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Evo.Infrastructure.Persistence.Repositories
{
    public class ThirdPartyDriverRepository : IThirdPartyDriverRepository
    {
        private readonly AppDbContext _ctx;
        public ThirdPartyDriverRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(ThirdPartyDriver entity, CancellationToken ct = default)
        {
            await _ctx.ThirdPartyDrivers.AddAsync(entity, ct);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task<ThirdPartyDriver?> GetByIdAsync(Guid driverId, bool includeThirdParty = false, CancellationToken ct = default)
        {
            // 'includeThirdParty' ignored after merge; no navigation remains
            return await _ctx.ThirdPartyDrivers.FirstOrDefaultAsync(d => d.DriverId == driverId, ct);
        }

        public async Task UpdateAsync(ThirdPartyDriver entity, CancellationToken ct = default)
        {
            _ctx.ThirdPartyDrivers.Update(entity);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(Guid driverId, CancellationToken ct = default)
        {
            var entity = await _ctx.ThirdPartyDrivers.FirstOrDefaultAsync(x => x.DriverId == driverId, ct);
            if (entity is null) return;
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<ThirdPartyDriver>> ListAsync(
            DriverStatus? status = null,
            DriverVerificationStatus? verification = null,
            bool? isAvailable = null,
            CancellationToken ct = default)
        {
            var q = _ctx.ThirdPartyDrivers.AsQueryable();

            if (status.HasValue) q = q.Where(d => d.Status == status);
            if (verification.HasValue) q = q.Where(d => d.VerificationStatus == verification);
            if (isAvailable.HasValue) q = q.Where(d => d.IsAvailable == isAvailable);

            return await q.OrderByDescending(d => d.CreatedAt).ToListAsync(ct);
        }
    }
}
