using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.ThirdPartyDrivers.Queries.List
{
    public class ListThirdPartyDriversQueryHandler : IRequestHandler<ListThirdPartyDriversQuery, IReadOnlyList<ThirdPartyDriverDto>>
    {
        private readonly IThirdPartyDriverRepository _repo;
        private readonly IMapper _mapper;

        public ListThirdPartyDriversQueryHandler(IThirdPartyDriverRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ThirdPartyDriverDto>> Handle(ListThirdPartyDriversQuery request, CancellationToken cancellationToken)
        {
            var items = await _repo.ListAsync(request.Status, request.VerificationStatus, request.IsAvailable, cancellationToken);
            return items.Select(_mapper.Map<ThirdPartyDriverDto>).ToList();
        }
    }
}
