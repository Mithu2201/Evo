using Evo.Application.Features.Customers.Dtos;
using FluentValidation;

namespace Evo.Application.Features.Customers.Validators
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
    {
        private const string NamePattern = @"^[\p{L}\p{M}][\p{L}\p{M}\.'\- ]{1,149}$";
        private const string E164Pattern = @"^\+?[1-9]\d{7,14}$";

        public UpdateCustomerValidator()
        {
            // ----- Id -----
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Customer Id is required.");

            // ----- Name -----
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(2, 150)
                .Matches(NamePattern)
                .WithMessage("Name can contain letters, spaces, apostrophes, dots and hyphens.");

            // ----- Phone (optional) -----
            RuleFor(x => x.Phone)
                .Matches(E164Pattern)
                .When(x => !string.IsNullOrWhiteSpace(x.Phone))
                .WithMessage("Phone must be in international format (E.164), e.g., +9477xxxxxxx.");

            // ----- Address fields (optional, max length) -----
            RuleFor(x => x.AddressLine1).MaximumLength(200);
            RuleFor(x => x.AddressLine2).MaximumLength(200);
            RuleFor(x => x.City).MaximumLength(100);
            RuleFor(x => x.District).MaximumLength(100);
            RuleFor(x => x.PostalCode).MaximumLength(20);
            RuleFor(x => x.Country).MaximumLength(100);

            // ----- DateOfBirth (optional, must be in the past) -----
            RuleFor(x => x.DateOfBirth)
                .Must(d => d == null || d.Value.Date < DateTime.UtcNow.Date)
                .WithMessage("Date of birth must be in the past.");
        }
    }
}
