using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Mappings
{
    public class GestionDesLivresProfile : Profile
    {
        public GestionDesLivresProfile()
        {
            CreateMap<Livre, LivreDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CategorieId, opt => opt.MapFrom(src => src.IDCategorie))
                .ForMember(dest => dest.Titre, opt => opt.MapFrom(src => src.Titre))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.Auteur, opt => opt.MapFrom(src => src.Auteur))
                .ForMember(dest => dest.Libelle, opt => opt.MapFrom(src => src.Categorie.Libelle));


            CreateMap<Categorie, CategorieDto>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                   .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                   .ForMember(dest => dest.Libelle, opt => opt.MapFrom(src => src.Libelle));
        }
    }
}
