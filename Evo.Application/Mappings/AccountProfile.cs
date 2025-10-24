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
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Token, opt => opt.Ignore()); // Token is set manually after creation

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


        }


    }
}
