using Evo.Application.Contracts.Persistence;
using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Delete
{
 public class DeleteThirdPartyDriverCommandHandler : IRequestHandler<DeleteThirdPartyDriverCommand, bool>
 {
 private readonly IThirdPartyDriverRepository _repo;
 public DeleteThirdPartyDriverCommandHandler(IThirdPartyDriverRepository repo) => _repo = repo;

 public async Task<bool> Handle(DeleteThirdPartyDriverCommand request, CancellationToken cancellationToken)
 {
 var entity = await _repo.GetByIdAsync(request.DriverId, ct: cancellationToken);
 if (entity is null) return false;
 await _repo.SoftDeleteAsync(request.DriverId, cancellationToken);
 return true;
 }
 }
}
