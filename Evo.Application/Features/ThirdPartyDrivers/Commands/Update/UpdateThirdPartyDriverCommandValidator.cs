using FluentValidation;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Update
{
 public class UpdateThirdPartyDriverCommandValidator : AbstractValidator<UpdateThirdPartyDriverCommand>
 {
 public UpdateThirdPartyDriverCommandValidator()
 {
 RuleFor(x => x.DriverId).NotEmpty();

 // Optional fields: validate format/length only when provided
 When(x => x.Changes is not null, () =>
 {
 RuleFor(x => x.Changes!.FirstName).MaximumLength(100);
 RuleFor(x => x.Changes!.LastName).MaximumLength(100);
 RuleFor(x => x.Changes!.WorkEmail).EmailAddress().MaximumLength(200).When(x => x.Changes!.WorkEmail != null);
 RuleFor(x => x.Changes!.PhoneE164).MaximumLength(20).When(x => x.Changes!.PhoneE164 != null);
 RuleFor(x => x.Changes!.NIC).MaximumLength(20).When(x => x.Changes!.NIC != null);
 RuleFor(x => x.Changes!.PhotoUrl).MaximumLength(500).When(x => x.Changes!.PhotoUrl != null);
 RuleFor(x => x.Changes!.VehicleType).MaximumLength(30).When(x => x.Changes!.VehicleType != null);
 RuleFor(x => x.Changes!.VehiclePlateNo).MaximumLength(30).When(x => x.Changes!.VehiclePlateNo != null);
 });
 }
 }
}
