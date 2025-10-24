using AutoMapper;
using Evo.API.Models.Requests.Accounts;
using Evo.API.Models.Requests.ThirdPartyDrivers;
using Evo.Application.DTOs;
using Evo.Application.Features.Accounts.Commands.GoogleLogin;
using Evo.Application.Features.Accounts.Commands.LoginUser;
using Evo.Application.Features.Accounts.Commands.RegisterAdminUser;
using Evo.Application.Features.Accounts.Commands.RegisterCustomerUser;
using Evo.Application.Features.Accounts.Commands.RegisterServiceProviderUser;
using Evo.Application.Features.Accounts.Commands.RegisterStaffUser;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Application.Features.Accounts.Requests;
using Evo.Application.Features.ThirdPartyDrivers.Commands.Create;
using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using Microsoft.AspNetCore.Identity.Data;


namespace Evo.API.Mappings
{
    public class ApiMappingProfile : Profile
    {

        public ApiMappingProfile()
        {
            // Map API request to DTO
            // This tells AutoMapper how to convert a RegisterRequest (from the API) into a RegisterDto.
            // It’s needed because the RegisterUserCommand contains a RegisterDto property, so AutoMapper must know how to populate it.
            CreateMap<RegisterCustomerApiRequest, RegisterCustomerUserDto>();
            CreateMap<RegisterStaffApiRequest, RegisterStaffUserDto>();
            CreateMap<RegisterAdminApiRequest, RegisterAdminUserDto>()
                .ForMember(d => d.Position, o => o.MapFrom(s => s.AdminPosition))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
            CreateMap<CreateThirdPartyApiRequest, CreateThirdPartyDriverDto>();


            // Map API request to Command and fill its RegisterDto property
            // This maps RegisterRequest directly to RegisterUserCommand. 
            // The ForMember part tells AutoMapper: "Take the RegisterRequest itself, map it into the RegisterDto property of the command."
            CreateMap<RegisterCustomerApiRequest, RegisterCustomerUserCommand>()
                    .ForMember(dest => dest.RegisterCustomerUserDto, opt => opt.MapFrom(src => src));
            
            CreateMap<RegisterStaffApiRequest, RegisterStaffUserCommand>()
                    .ForMember(dest => dest.RegisterStaffUserDto, opt => opt.MapFrom(src => src));
            
            CreateMap<RegisterAdminApiRequest, RegisterAdminUserCommand>()
                    .ForMember(dest => dest.RegisterAdminUserDto, opt => opt.MapFrom(src => src));

            CreateMap<CreateThirdPartyApiRequest, CreateThirdPartyDriverCommand>()
                    .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => src));

            // Google login mapping
            CreateMap<GoogleLoginApiRequest, GoogleLoginCommand>()
                .ForMember(dest => dest.IdToken, opt => opt.MapFrom(src => src.IdToken));


            CreateMap<RegisterServiceProviderApiRequest, RegisterServiceProviderUserDto>();

            CreateMap<RegisterServiceProviderApiRequest, RegisterServiceProviderUserCommand>()
                .ForMember(dest => dest.RegisterServiceProviderUserDto, opt => opt.MapFrom(src => src));


            CreateMap<LoginRequest, LoginDto>();

            CreateMap<LoginRequest, LoginUserCommand>()
               .ForMember(dest => dest.LoginDto, opt => opt.MapFrom(src => src));

        }

    }
}
