using AutoMapper;
using Evo.API.Models.Requests.Accounts;
using Evo.Application.Features.Accounts.Commands.RegisterCustomerUser;
using Evo.Application.Features.Accounts.Dtos;


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


            // Map API request to Command and fill its RegisterDto property
            // This maps RegisterRequest directly to RegisterUserCommand. 
            // The ForMember part tells AutoMapper: "Take the RegisterRequest itself, map it into the RegisterDto property of the command."
            CreateMap<RegisterCustomerApiRequest, RegisterCustomerUserCommand>()
                    .ForMember(dest => dest.RegisterCustomerUserDto, opt => opt.MapFrom(src => src));


        }

    }
}
