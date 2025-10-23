using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using FluentValidation;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Create
{
    public class CreateThirdPartyDriverCommandValidator : AbstractValidator<CreateThirdPartyDriverDto>
    {
        public CreateThirdPartyDriverCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.WorkEmail).NotEmpty().EmailAddress().MaximumLength(200);
            RuleFor(x => x.PhoneE164).NotEmpty().MaximumLength(20);
        }
    }
}
