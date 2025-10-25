using Evo.Application.Features.Customers.Dtos;
using MediatR;

namespace Evo.Application.Features.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public UpdateCustomerDto UpdateCustomerDto { get; set; } = default!;
    }
}