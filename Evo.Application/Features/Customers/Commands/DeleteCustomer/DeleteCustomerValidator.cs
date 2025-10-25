using Evo.Application.Features.Customers.Dtos;
using FluentValidation;

namespace Evo.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
