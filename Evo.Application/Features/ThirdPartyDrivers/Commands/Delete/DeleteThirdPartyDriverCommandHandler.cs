using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.ThirdPartyDrivers.Commands.Delete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.DeleteThirdPartyDriver
{
    public class DeleteThirdPartyDriverCommandHandler
        : IRequestHandler<DeleteThirdPartyDriverCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public DeleteThirdPartyDriverCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteThirdPartyDriverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.ThirdPartyDrivers.GetByIdAsync(request.DriverId, ct: cancellationToken);

            if (entity == null)
                return false;

            await _uow.ThirdPartyDrivers.SoftDeleteAsync(request.DriverId, cancellationToken);

            return true;
        }
    }
}
