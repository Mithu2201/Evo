using Evo.Application.Features.Customers.Dtos;
using MediatR;

namespace Evo.Application.Features.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public string Id { get; set; } = default!;
    }
}
