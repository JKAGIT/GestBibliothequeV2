using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class ObtenirTousLivresQueryHandler : IRequestHandler<ObtenirTousLivresQuery, IEnumerable<LivreDto>>
    {
        private readonly ILivreRepository _livreRepository;

        public ObtenirTousLivresQueryHandler(ILivreRepository livreRepository)
        {
            _livreRepository = livreRepository;
        }

        public async Task<IEnumerable<LivreDto>> Handle(ObtenirTousLivresQuery request, CancellationToken cancellationToken)
        {
            var livres = await _livreRepository.ObtenirLivresAvecCategories();

            return livres.Select(livre => new LivreDto
            {
                Id = livre.ID,
                Titre = livre.Titre,
                Auteur = livre.Auteur,
                CategorieId = livre.IDCategorie,
                Libelle = livre.Categorie?.Libelle,
                Stock = livre.Stock
            });
        }
    }
}
