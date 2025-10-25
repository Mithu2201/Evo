using AutoMapper;
using Evo.Application.Contracts.Persistence;
using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Update
{
    public class UpdateThirdPartyDriverCommandHandler : IRequestHandler<UpdateThirdPartyDriverCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateThirdPartyDriverCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateThirdPartyDriverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.ThirdPartyDrivers.GetByIdAsync(request.DriverId, ct: cancellationToken);
            if (entity is null) return false;

            _mapper.Map(request.Changes, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            await _uow.ThirdPartyDrivers.UpdateAsync(entity, cancellationToken);
            return true;
        }
    }
}
