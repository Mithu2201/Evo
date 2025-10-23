using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Queries.GetById
{
    public class GetThirdPartyDriverByIdQueryHandler : IRequestHandler<GetThirdPartyDriverByIdQuery, ThirdPartyDriverDto?>
    {
        private readonly IThirdPartyDriverRepository _repo;
        private readonly IMapper _mapper;

        public GetThirdPartyDriverByIdQueryHandler(IThirdPartyDriverRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ThirdPartyDriverDto?> Handle(GetThirdPartyDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.DriverId, includeThirdParty: false, ct: cancellationToken);
            return entity is null ? null : _mapper.Map<ThirdPartyDriverDto>(entity);
        }
    }
}
