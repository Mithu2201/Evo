using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.Customers.Commands;
using MediatR;

namespace Evo.Application.Features.Customers.Handlers;

public class DeleteCustomerHandler(ICustomerRepository repository)
    : IRequestHandler<DeleteCustomerCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        // Soft delete the customer
        await repository.SoftDeleteAsync(request.Id);

        // Save changes
        await repository.SaveChangesAsync();

        return Unit.Value;
    }
}
