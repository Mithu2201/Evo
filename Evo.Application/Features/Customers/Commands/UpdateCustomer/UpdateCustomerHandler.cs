using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.Customers.Commands;
using Evo.Application.Features.Customers.Dtos;
using MediatR;

namespace Evo.Application.Features.Customers.Handlers;

public class UpdateCustomerHandler(ICustomerRepository repository, IMapper mapper)
    : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.UpdateCustomerDto.Id);
        if (customer is null)
            throw new KeyNotFoundException($"Customer with ID {request.UpdateCustomerDto.Id} not found.");

        mapper.Map(request.UpdateCustomerDto, customer);
        customer.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(customer);
        await repository.SaveChangesAsync();

        return mapper.Map<CustomerDto>(customer);
    }
}
