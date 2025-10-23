using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using FluentValidation;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Create
{
    public class CreateThirdPartyDriverCommandValidator : AbstractValidator<CreateThirdPartyDriverCommand>
    {
        public CreateThirdPartyDriverCommandValidator()
        {
            RuleFor(x => x.Driver).NotNull();

            When(x => x.Driver is not null, () =>
            {
                RuleFor(x => x.Driver!.FirstName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Driver!.WorkEmail).NotEmpty().EmailAddress().MaximumLength(200);
                RuleFor(x => x.Driver!.PhoneE164).NotEmpty().MaximumLength(20);

                RuleFor(x => x.Driver!.LastName).MaximumLength(100).When(x => x.Driver!.LastName != null);
                RuleFor(x => x.Driver!.NIC).MaximumLength(20).When(x => x.Driver!.NIC != null);
                RuleFor(x => x.Driver!.PhotoUrl).MaximumLength(500).When(x => x.Driver!.PhotoUrl != null);
            });
        }
    }
}
