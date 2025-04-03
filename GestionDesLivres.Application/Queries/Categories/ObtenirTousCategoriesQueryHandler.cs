using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionDesLivres.Application.Queries.Categories
{
    public class ObtenirTousCategoriesQueryHandler : IRequestHandler<ObtenirTousCategoriesQuery, IEnumerable<CategorieDto>>
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IRecherche<Categorie> _recherche;

        public ObtenirTousCategoriesQueryHandler(ICategorieRepository categorieRepository, IRecherche<Categorie> recherche)
        {
            _categorieRepository = categorieRepository;
            _recherche = recherche;
        }

        public async Task<IEnumerable<CategorieDto>> Handle(ObtenirTousCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _recherche.GetAll()
                                              .Include(c => c.Livres)
                                              .ToListAsync();

            var categoriesDto = categories.Select(categorie => new CategorieDto
            {
                Id = categorie.ID,
                Code = categorie.Code,
                Libelle = categorie.Libelle,
                Livres = categorie.Livres.Select(livre => new LivreDto
                {
                    Id = livre.ID,
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    Stock=livre.Stock,
                    CategorieId = categorie.ID
                })
            });
            return categoriesDto;
        }
    }
}
