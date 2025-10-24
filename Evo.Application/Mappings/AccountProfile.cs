using AutoMapper;
using Evo.Application.DTOs;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Domain.Entities;
using Evo.Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Evo.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {

            // Map Domain → DTO
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RolePermissions.ToString()))
                .ForMember(dest => dest.Token, opt => opt.Ignore());


            CreateMap<RegisterCustomerUserDto, User>()
               // Map simple fields
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions ?? Evo.Domain.Enums.UserRole.Customer))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? true))

               // Ignore password fields here, we will set them in AfterMap
               .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
               .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())

               // Ignore navigation properties
               .ForMember(dest => dest.Customer, opt => opt.Ignore())

               // AfterMap to handle password hashing
               .AfterMap((src, dest) =>
               {
                   using var hmac = new HMACSHA512();
                   dest.PasswordSalt = hmac.Key;
                   dest.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(src.Password));
               });



            // Map from DTO -> Customer entity
            CreateMap<RegisterCustomerUserDto, Customer>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.CustomerPhone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => AccountStatus.Active))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.AddressLine1))
                .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.AddressLine2))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore()) // will be linked after user creation
                .ForMember(dest => dest.UserId, opt => opt.Ignore()); // set later in handler




            CreateMap<RegisterStaffUserDto, User>()
               // User basics
               .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
               .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
               .ForMember(d => d.RolePermissions, o => o.MapFrom(_ => UserRole.Staff)) // or whatever role enum fits Staff
                                                                                       // ignore these; we set in AfterMap
               .ForMember(d => d.PasswordHash, o => o.Ignore())
               .ForMember(d => d.PasswordSalt, o => o.Ignore())
               // navs
               .ForMember(d => d.Customer, o => o.Ignore())
               //  .ForMember(d => d.ServiceProvider, o => o.Ignore())
               .ForMember(d => d.Staff, o => o.Ignore())
               .AfterMap((src, dest) =>
               {
                   using var hmac = new HMACSHA512();
                   dest.PasswordSalt = hmac.Key;
                   dest.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(src.Password));
               });

            // DTO -> Staff
            CreateMap<RegisterStaffUserDto, Staff>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.Phone))
                .ForMember(d => d.AddressLine1, o => o.MapFrom(s => s.AddressLine1))
                .ForMember(d => d.AddressLine2, o => o.MapFrom(s => s.AddressLine2))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City))
                .ForMember(d => d.District, o => o.MapFrom(s => s.District))
                .ForMember(d => d.PostalCode, o => o.MapFrom(s => s.PostalCode))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.Position))
                .ForMember(d => d.Department, o => o.MapFrom(s => s.Department))
                .ForMember(d => d.Permissions, o => o.MapFrom(s => s.Permissions))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .ForMember(d => d.HireDate, o => o.MapFrom(s => s.HireDate == default ? DateTime.UtcNow : s.HireDate))
                // set/ignore runtime fields
                .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.EndDate, o => o.Ignore())
                //.ForMember(d => d.RowVersion, o => o.Ignore())
                .ForMember(d => d.User, o => o.Ignore())
                .ForMember(d => d.UserId, o => o.Ignore()); // set in handler after creating User


            CreateMap<RegisterAdminUserDto, User>()
           // Simple fields
           
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == AccountStatus.Active))
           // If your enum name differs, change UserRole.Admin accordingly.
           .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(_ => UserRole.Admin))

           // Ignore password fields; set them in AfterMap
           .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
           .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())

           // (Optional) Ignore nav properties if present on User
           // .ForMember(dest => dest.Customer, opt => opt.Ignore())
           // .ForMember(dest => dest.Staff,    opt => opt.Ignore())
           // .ForMember(dest => dest.Admin,    opt => opt.Ignore())

           // Hash password
           .AfterMap((src, dest) =>
           {
               using var hmac = new HMACSHA512();
               dest.PasswordSalt = hmac.Key;
              
           });

            // --- RegisterAdminUserDto → Admin ---
            CreateMap<RegisterAdminUserDto, Admin>()
                .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.StaffId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Staff, opt => opt.Ignore()) // linked by StaffId/EF
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // let EF/default ctor handle


            // DTO -> User
            CreateMap<RegisterServiceProviderUserDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                // Respect incoming role if provided; default to ServiceProvider
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions ?? UserRole.ServiceProvider))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? true))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceProvider, opt => opt.Ignore()) // link after creation
                .AfterMap((src, dest) =>
                {
                    using var hmac = new HMACSHA512(); // Consider PBKDF2/BCrypt/Argon2 in production
                    dest.PasswordSalt = hmac.Key;
                    dest.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(src.Password));
                });

            // DTO -> ServiceProvider
            CreateMap<RegisterServiceProviderUserDto, ServiceProvider>()
                // Core identity
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))

                // Address (granular)
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.AddressLine1))
                .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.AddressLine2))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))

                // Status / verification (DTO has no property; default to Pending)
                .ForMember(dest => dest.VerificationStatus, opt => opt.MapFrom(_ => VerificationStatus.Pending))

                // Operational params (map only when provided)
                .ForMember(dest => dest.MaxConcurrentBookings, opt =>
                {
                    opt.PreCondition(src => src.MaxConcurrentBookings.HasValue);
                    opt.MapFrom(src => src.MaxConcurrentBookings!.Value);
                })
                .ForMember(dest => dest.MinLeadTimeDays, opt =>
                {
                    opt.PreCondition(src => src.MinLeadTimeDays.HasValue);
                    opt.MapFrom(src => src.MinLeadTimeDays!.Value);
                })
                .ForMember(dest => dest.BookingWindowDays, opt =>
                {
                    opt.PreCondition(src => src.BookingWindowDays.HasValue);
                    opt.MapFrom(src => src.BookingWindowDays!.Value);
                })
                .ForMember(dest => dest.CreditPeriod, opt =>
                {
                    opt.PreCondition(src => src.CreditPeriod.HasValue);
                    opt.MapFrom(src => src.CreditPeriod!.Value);
                })

                // Business docs / policies
                .ForMember(dest => dest.BusinessLicense, opt => opt.MapFrom(src => src.BusinessLicense))
                .ForMember(dest => dest.TaxId, opt => opt.MapFrom(src => src.TaxId))
                .ForMember(dest => dest.CancellationPolicy, opt => opt.MapFrom(src => src.CancellationPolicy ?? "{}"))
                .ForMember(dest => dest.PaymentMethods, opt => opt.MapFrom(src => src.PaymentMethods ?? "[]"))

                // Navigation & concurrency
                .ForMember(dest => dest.User, opt => opt.Ignore()) // set UserId in handler
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }


    }
}
