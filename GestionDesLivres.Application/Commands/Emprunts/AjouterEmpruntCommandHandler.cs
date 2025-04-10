using GestionDesLivres.Application.Commands.Livres;
using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Emprunts
{
    public class AjouterEmpruntCommandHandler : IRequestHandler<AjouterEmpruntCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILivreRepository _livreRepository;

        public AjouterEmpruntCommandHandler(IUnitOfWork unitOfWork, ILivreRepository livreRepository)
        {
            _unitOfWork = unitOfWork;
            _livreRepository = livreRepository;
        }

        public async Task<Guid> Handle(AjouterEmpruntCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();
                if (request.IDLivre == Guid.Empty || request.IDLivre == Guid.Empty)
                    validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

                var emprunt = new Emprunt
                {
                    ID = Guid.NewGuid(),
                    IDUsager = request.IDUsager,
                    IDLivre = request.IDLivre,
                    DateDebut = request.DateDebut,
                    DateRetourPrevue = request.DateRetourPrevue,
                    IDReservation = request.IDReservation
                };

                await _unitOfWork.Emprunts.AddAsync(emprunt);
                await _livreRepository.MettreAJourStock(emprunt.IDLivre, -1);
                await _unitOfWork.CompleteAsync();

                return emprunt.ID;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur inattendue s'est produite.", ex);
            }
        }
    }
}
