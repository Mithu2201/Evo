using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.Customers.Dtos;
using Evo.Application.Features.Customers.Queries;
using MediatR;
using System.Collections.Generic;

namespace Evo.Application.Features.Customers.Handlers;

public class GetAllCustomersHandler(ICustomerRepository repository, IMapper mapper)
    : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
}
