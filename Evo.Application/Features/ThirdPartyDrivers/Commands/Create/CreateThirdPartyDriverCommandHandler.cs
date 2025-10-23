using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.ThirdPartyDrivers.ViewModels;
using Evo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Create
{
    public class CreateThirdPartyDriverCommandHandler : IRequestHandler<CreateThirdPartyDriverCommand, ThirdPartyDriverVm>
    {
        private readonly IThirdPartyDriverRepository _repo;
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public CreateThirdPartyDriverCommandHandler(
            IThirdPartyDriverRepository repo,
            IUserRepository users,
            IMapper mapper)
        {
            _repo = repo;
            _users = users;
            _mapper = mapper;
        }

        public async Task<ThirdPartyDriverVm> Handle(CreateThirdPartyDriverCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ThirdPartyDriver>(request.Driver);

            // Validate optional UserId to avoid FK violation
            if (!string.IsNullOrWhiteSpace(entity.UserId))
            {
                var user = await _users.GetUserByIdAsync(entity.UserId);
                if (user is null)
                {
                    // If the provided UserId doesn't exist, unlink the driver to prevent FK constraint errors
                    entity.UserId = null;
                }
            }

            await _repo.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<Evo.Application.Features.ThirdPartyDrivers.Dtos.ThirdPartyDriverDto>(entity);
            return new ThirdPartyDriverVm { Driver = dto };
        }
    }
}
