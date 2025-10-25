using MediatR;

namespace Evo.Application.Features.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
