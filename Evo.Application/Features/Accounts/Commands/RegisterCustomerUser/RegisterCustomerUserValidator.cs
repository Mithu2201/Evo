using Evo.Application.Features.Accounts.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterCustomerUser
{
    public class RegisterCustomerUserValidator : AbstractValidator<RegisterCustomerUserCommand>
    {
        // Patterns taken from your entities
        private const string NamePattern = @"^[\p{L}\p{M}][\p{L}\p{M}\.'\- ]{1,149}$";
        private const string E164Pattern = @"^\+?[1-9]\d{7,14}$";

       
        public RegisterCustomerUserValidator()
        {

            // ----- User.Email -----
            RuleFor(x => x.RegisterCustomerUserDto.Email)
                .NotEmpty()
                .EmailAddress()
                .MinimumLength(5)
                .MaximumLength(100);

            // ----- Passwords -----
            RuleFor(x => x.RegisterCustomerUserDto.Password)
                .NotEmpty()
                .MinimumLength(8)    // Choose your policy; entity only constrains hash length
                .MaximumLength(128);

            //RuleFor(x => x.RegisterCustomerUserDto.ConfirmPassword)
            //    .Equal(x => x.RegisterCustomerUserDto.Password)
            //    .WithMessage("Passwords do not match.");

            // ----- Customer.Name -----
            RuleFor(x =>x.RegisterCustomerUserDto.CustomerName)
                .NotEmpty()
                .Length(2, 150)
                .Matches(NamePattern)
                .WithMessage("Name can contain letters, spaces, apostrophes, dots and hyphens.");

            // ----- Customer.Phone -----
            RuleFor(x => x.RegisterCustomerUserDto.CustomerPhone)
                .NotEmpty()
                .Length(7, 20)
                .Matches(E164Pattern)
                .WithMessage("Phone must be in international format (E.164), e.g., +9477xxxxxxx.");

            // ----- Customer.Email -----
            //RuleFor(x => x.RegisterCustomerUserDto.CustomerEmail)
            //    .NotEmpty()
            //    .EmailAddress()
            //    .MaximumLength(100);

            // ----- Addresses -----
            RuleFor(x => x.RegisterCustomerUserDto.AddressLine1).MaximumLength(200);
            RuleFor(x => x.RegisterCustomerUserDto.AddressLine2).MaximumLength(200);
            RuleFor(x => x.RegisterCustomerUserDto.City).MaximumLength(100);
            RuleFor(x => x.RegisterCustomerUserDto.District).MaximumLength(100);
            RuleFor(x => x.RegisterCustomerUserDto.PostalCode).MaximumLength(20);
            RuleFor(x => x.RegisterCustomerUserDto.Country).MaximumLength(100);

            // ----- DateOfBirth: must be in the past if provided -----
            RuleFor(x => x.RegisterCustomerUserDto.DateOfBirth)
                .Must(d => d == null || d.Value.Date < DateTime.UtcNow.Date)
                .WithMessage("Date of birth must be in the past.");

            // Optional enums are validated when supplied (handler can default otherwise)
            RuleFor(x => x.RegisterCustomerUserDto.Status)
                .IsInEnum()
                .When(x => x.RegisterCustomerUserDto.Status.HasValue);

            // No rule for RolePermissions here; handler can default to Customer if null.
        }
    }
}
