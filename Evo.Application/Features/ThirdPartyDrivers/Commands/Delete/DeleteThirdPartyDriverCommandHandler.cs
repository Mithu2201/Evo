using Evo.Application.Contracts.Persistence;
using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Delete
{
 public class DeleteThirdPartyDriverCommandHandler : IRequestHandler<DeleteThirdPartyDriverCommand, bool>
 {
 private readonly IUnitOfWork _uow;
 public DeleteThirdPartyDriverCommandHandler(IUnitOfWork uow) => _uow = uow;

 public async Task<bool> Handle(DeleteThirdPartyDriverCommand request, CancellationToken cancellationToken)
 {
 var entity = await _uow.ThirdPartyDrivers.GetByIdAsync(request.DriverId, ct: cancellationToken);
 if (entity is null) return false;
 await _uow.ThirdPartyDrivers.SoftDeleteAsync(request.DriverId, cancellationToken);
 return true;
 }
 }
}
