using System.Text.Json;
using System.Text.RegularExpressions;
using Evo.Application.Features.Accounts.Dtos;
using FluentValidation;

namespace Evo.Application.Features.Accounts.Validation
{
    /// <summary>
    /// FluentValidation rules for RegisterServiceProviderUserDto.
    /// Compatible with older FluentValidation (no .Transform()).
    /// </summary>
    public class RegisterServiceProviderUserDtoValidator : AbstractValidator<RegisterServiceProviderUserDto>
    {
        // E.164: +[country][national] total 7..15 digits
        private static readonly Regex E164 = new Regex(@"^\+[1-9]\d{6,14}$", RegexOptions.Compiled);

        public RegisterServiceProviderUserDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            // ---------- Account ----------
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.")
                .MaximumLength(100)
                // Disallow leading/trailing spaces without mutating the model
                .Must(v => v == null || v == v.Trim())
                    .WithMessage("Email cannot start or end with whitespace.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6)
                .MaximumLength(100);
            // Optional strength checks (uncomment if needed)
            // .Matches(@"[A-Z]").WithMessage("Password must contain an uppercase letter.")
            // .Matches(@"[a-z]").WithMessage("Password must contain a lowercase letter.")
            // .Matches(@"\d").WithMessage("Password must contain a number.")
            // .Matches(@"[^\w\s]").WithMessage("Password must contain a symbol.");

            RuleFor(x => x.RolePermissions)
                .IsInEnum().When(x => x.RolePermissions.HasValue);

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive must be provided.");

            // ---------- Service Provider ----------
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .MaximumLength(200)
                .Must(v => v == null || v == v.Trim())
                    .WithMessage("CompanyName cannot start or end with whitespace.");

            RuleFor(x => x.BrandName)
                .NotEmpty()
                .MaximumLength(200)
                .Must(v => v == null || v == v.Trim())
                    .WithMessage("BrandName cannot start or end with whitespace.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(7)
                .Must(v => v != null && E164.IsMatch(v.Trim()))
                    .WithMessage("Phone must be E.164 format, e.g. +9477XXXXXXX");

            RuleFor(x => x.Description)
                .MaximumLength(2000);

            // Address
            RuleFor(x => x.AddressLine1).MaximumLength(200);
            RuleFor(x => x.AddressLine2).MaximumLength(200);
            RuleFor(x => x.City).MaximumLength(100);
            RuleFor(x => x.District).MaximumLength(100);
            RuleFor(x => x.PostalCode).MaximumLength(20);
            RuleFor(x => x.Country).MaximumLength(100);

            // Operational settings (non-negative)
            RuleFor(x => x.MaxConcurrentBookings)
                .GreaterThanOrEqualTo(0).When(x => x.MaxConcurrentBookings.HasValue)
                .WithMessage("MaxConcurrentBookings cannot be negative.");

            RuleFor(x => x.MinLeadTimeDays)
                .GreaterThanOrEqualTo(0).When(x => x.MinLeadTimeDays.HasValue)
                .WithMessage("MinLeadTimeDays cannot be negative.");

            RuleFor(x => x.BookingWindowDays)
                .GreaterThanOrEqualTo(0).When(x => x.BookingWindowDays.HasValue)
                .WithMessage("BookingWindowDays cannot be negative.");

            RuleFor(x => x.CreditPeriod)
                .GreaterThanOrEqualTo(0).When(x => x.CreditPeriod.HasValue)
                .WithMessage("CreditPeriod cannot be negative.");

            // Cross-field: booking window must cover lead time
            RuleFor(x => x)
                .Must(x => !x.BookingWindowDays.HasValue || !x.MinLeadTimeDays.HasValue || x.BookingWindowDays >= x.MinLeadTimeDays)
                .WithMessage("BookingWindowDays must be greater than or equal to MinLeadTimeDays.");

            RuleFor(x => x.BusinessLicense)
                .MaximumLength(100);

            RuleFor(x => x.TaxId)
                .MaximumLength(50);

            // JSON strings
            RuleFor(x => x.CancellationPolicy)
                .Must(BeValidJsonObject)
                .WithMessage("CancellationPolicy must be a valid JSON object string (e.g., {\"rules\":[]}).");

            RuleFor(x => x.PaymentMethods)
                .Must(BeValidJsonArray)
                .WithMessage("PaymentMethods must be a valid JSON array string (e.g., [\"card\",\"cash\"]).");
        }

        private static bool BeValidJsonObject(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return true; // optional
            try
            {
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.ValueKind == JsonValueKind.Object;
            }
            catch { return false; }
        }

        private static bool BeValidJsonArray(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return true; // optional
            try
            {
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.ValueKind == JsonValueKind.Array;
            }
            catch { return false; }
        }
    }
}

// Registration in DI (e.g., in Startup/Program):
// services.AddValidatorsFromAssemblyContaining<RegisterServiceProviderUserDtoValidator>();