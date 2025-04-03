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
    public class MettreAJourUsagerCommandHandler : IRequestHandler<MettreAJourUsagerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Usager> _entityValidationService;
        private readonly IUsagerRepository _usagerRepository;
        //private readonly IRecherche<Emprunts> _rechercheEmprunt;
        public MettreAJourUsagerCommandHandler(IUnitOfWork unitOfWork, IEntityValidationService<Usager> entityValidationService, IUsagerRepository usagerRepository)
        {
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _usagerRepository = usagerRepository;
        }

        public async Task<bool> Handle(MettreAJourUsagerCommand request, CancellationToken cancellationToken)
        {
            List<string> validationErrors = new List<string>();
            if (string.IsNullOrEmpty(request.Nom) || string.IsNullOrEmpty(request.Prenoms) || string.IsNullOrEmpty(request.Telephone) || string.IsNullOrEmpty(request.Courriel))
                validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

            var usager = await _unitOfWork.Usagers.GetByIdAsync(request.Id);
            if (usager == null)
                throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Usager", request.Id));

            usager.Nom = request.Nom;
            usager.Prenoms = request.Prenoms;
            usager.Courriel = request.Courriel;
            usager.Telephone = request.Telephone;

            if (!usager.EstValide())
                throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

            await _usagerRepository.UpdateAsync(usager);
            await _unitOfWork.CompleteAsync();
            return true;

        }
    }
}
