using AutoMapper;
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

        }


    }
}
