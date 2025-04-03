using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Application.Queries.Livres;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Categories
{
    public class ObtenirCategorieParIdQueryHandler : IRequestHandler<ObtenirCategorieParIdQuery, CategorieDto>
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IMapper _mapper;

        public ObtenirCategorieParIdQueryHandler(ICategorieRepository categorieRepository, IMapper mapper)
        {
            _categorieRepository = categorieRepository;
            _mapper = mapper;
        }

        public async Task<CategorieDto> Handle(ObtenirCategorieParIdQuery request, CancellationToken cancellationToken)
        {
            var categorie =  await _categorieRepository.GetByIdAsync(request.CategorieId);
            if (categorie == null)
                throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.CategorieId));
            return _mapper.Map<CategorieDto>(categorie);
        }
    }
}
