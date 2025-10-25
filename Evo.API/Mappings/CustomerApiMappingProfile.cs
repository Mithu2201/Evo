using AutoMapper;
using Evo.API.Models.Requests.Customers;
using Evo.Application.Features.Customers.Commands;
using Evo.Application.Features.Customers.Dtos;

namespace Evo.API.Mappings
{
    public class CustomerApiMappingProfile : Profile
    {
        public CustomerApiMappingProfile()
        {
            // Update
            CreateMap<UpdateCustomerApiRequest, UpdateCustomerCommand>()
                .ForMember(dest => dest.UpdateCustomerDto, opt => opt.MapFrom(src => src));

            // Delete
            //CreateMap<DeleteCustomerApiRequest, DeleteCustomerCommand>();

            // (Optional) If you want DTO mapping
            CreateMap<UpdateCustomerApiRequest, UpdateCustomerDto>();
        }
    }
}
