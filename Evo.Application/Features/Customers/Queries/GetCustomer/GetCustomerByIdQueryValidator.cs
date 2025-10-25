using Evo.Application.Features.Customers.Queries;
using FluentValidation;

namespace Evo.Application.Features.Customers.Validators;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID is required.");
    }
}
