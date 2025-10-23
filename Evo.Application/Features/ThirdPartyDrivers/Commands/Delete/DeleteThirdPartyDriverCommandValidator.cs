using FluentValidation;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Delete
{
 public class DeleteThirdPartyDriverCommandValidator : AbstractValidator<DeleteThirdPartyDriverCommand>
 {
 public DeleteThirdPartyDriverCommandValidator()
 {
 RuleFor(x => x.DriverId).NotEmpty();
 }
 }
}
