using AutoMapper;
using Evo.Application.Contracts.Persistence;
using MediatR;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Update
{
    public class UpdateThirdPartyDriverCommandHandler : IRequestHandler<UpdateThirdPartyDriverCommand, bool>
    {
        private readonly IThirdPartyDriverRepository _repo;
        private readonly IMapper _mapper;

        public UpdateThirdPartyDriverCommandHandler(IThirdPartyDriverRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateThirdPartyDriverCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.DriverId, ct: cancellationToken);
            if (entity is null) return false;

            _mapper.Map(request.Changes, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(entity, cancellationToken);
            return true;
        }
    }
}
