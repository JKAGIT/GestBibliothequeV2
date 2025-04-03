using AutoMapper;
using GestionDesUsagers.Application.DTO;
using GestionDesUsagers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Application.Mappings
{
    public class GestionUsagersProfile : Profile
    {
        public GestionUsagersProfile()
        {
            CreateMap<Usager, UsagerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.Nom))
                .ForMember(dest => dest.Prenoms, opt => opt.MapFrom(src => src.Prenoms))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.Courriel, opt => opt.MapFrom(src => src.Courriel));
        }

    }
}
