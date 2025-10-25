using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.Customers.Dtos;
using Evo.Application.Features.Customers.Queries;
using MediatR;

namespace Evo.Application.Features.Customers.Handlers;

public class GetCustomerByIdHandler(ICustomerRepository repository, IMapper mapper)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id);
        if (customer is null)
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");

        return mapper.Map<CustomerDto>(customer);
    }
}
