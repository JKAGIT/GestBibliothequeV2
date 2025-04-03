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
    public class AjouterUsagerCommandHandler : IRequestHandler<AjouterUsagerCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Usager> _entityValidationService;
        private readonly IUsagerRepository _usagerRepository;
        //private readonly IRecherche<Emprunts> _rechercheEmprunt;
        public AjouterUsagerCommandHandler(IUnitOfWork unitOfWork, IEntityValidationService<Usager> entityValidationService,IUsagerRepository usagerRepository)
        {
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _usagerRepository = usagerRepository;
        }

        public async Task<Guid> Handle(AjouterUsagerCommand request, CancellationToken cancellationToken)
        {
            List<string> validationErrors = new List<string>();
            if (string.IsNullOrEmpty(request.Nom) || string.IsNullOrEmpty(request.Prenoms) || string.IsNullOrEmpty(request.Telephone) || string.IsNullOrEmpty(request.Courriel))
                validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

            if (await _entityValidationService.VerifierExistenceAsync(u => u.Courriel == request.Courriel))
                throw new ValidationException(ErreurMessageProvider.GetMessage("EntiteExisteDeja", "Un usager", request.Courriel));

            var usager = new Usager
            {
                ID = Guid.NewGuid(),
                Nom = request.Nom,
                Prenoms = request.Prenoms,
                Courriel = request.Courriel,
                Telephone= request.Telephone
                };
            if (!usager.EstValide())
                throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

            await _usagerRepository.AddAsync(usager);
            await _unitOfWork.CompleteAsync();

            return usager.ID;
        }
    }
}
