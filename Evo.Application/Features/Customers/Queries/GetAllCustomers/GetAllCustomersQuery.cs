using Evo.Application.Features.Customers.Dtos;
using MediatR;

namespace Evo.Application.Features.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
    }
}
