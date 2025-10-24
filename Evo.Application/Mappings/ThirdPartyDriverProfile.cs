using AutoMapper;
using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using Evo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Mappings
{
    public class ThirdPartyDriverProfile : Profile
    {
        public ThirdPartyDriverProfile()
        {
            CreateMap<ThirdPartyDriver, ThirdPartyDriverDto>();

            CreateMap<CreateThirdPartyDriverDto, ThirdPartyDriver>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.MapFrom(_ => false))
                .ForMember(d => d.DeletedAt, o => o.Ignore())
                .ForMember(d => d.RowVersion, o => o.Ignore());

            CreateMap<UpdateThirdPartyDriverDto, ThirdPartyDriver>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember is not null));
        }
    }
}
