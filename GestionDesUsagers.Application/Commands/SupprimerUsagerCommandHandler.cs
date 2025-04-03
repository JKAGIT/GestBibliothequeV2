using GestionDesUsagers.Domain;
using GestionDesUsagers.Domain.Common;
using GestionDesUsagers.Domain.Entities;
using GestionDesUsagers.Domain.Repositories;
using GestionDesUsagers.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Application.Commands
{
    public class SupprimerUsagerCommandHandler : IRequestHandler<SupprimerUsagerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Usager> _entityValidationService;
        private readonly IUsagerRepository _usagerRepository;
        //private readonly IRecherche<Emprunts> _rechercheEmprunt;
        public SupprimerUsagerCommandHandler(IUnitOfWork unitOfWork, IEntityValidationService<Usager> entityValidationService, IUsagerRepository usagerRepository)
        {
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _usagerRepository = usagerRepository;
        }

        public async Task<bool> Handle(SupprimerUsagerCommand request, CancellationToken cancellationToken)
        {
            var usager = await _unitOfWork.Usagers.GetByIdAsync(request.Id);
            if (usager == null)
                throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Usager", request.Id));
            await _usagerRepository.DeleteAsync(request.Id);
            await _unitOfWork.CompleteAsync();

            return true;

            #region  avec entité liée - A faire apres emprunt et reservation
            //var categorieASupprimer = await _recherche.GetAll()
            //                                                 .Include(c => c.Livres)
            //                                                 .FirstOrDefaultAsync(c => c.ID == request.Id);

            //if (categorieASupprimer == null)
            //    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.Id));

            //if (categorieASupprimer.Livres != null && categorieASupprimer.Livres.Any())
            //    throw new ValidationException(ErreurMessageProvider.GetMessage("ErreurSuppressionEntiteLiee", "une catégorie", "livres"));

            //await _categorieRepository.DeleteAsync(request.Id);
            //await _unitOfWork.CompleteAsync();

            //return true;
            #endregion
        }
    }
}
