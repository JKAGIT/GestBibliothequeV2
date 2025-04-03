using GestionDesLivres.Application.Commands.Livres;
using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Categories
{
    public class SupprimerLivreCommandHandler : IRequestHandler<SupprimerLivreCommand, bool>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupprimerLivreCommandHandler(ILivreRepository livreRepository, IUnitOfWork unitOfWork)
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SupprimerLivreCommand request, CancellationToken cancellationToken)
        {
            var livre = await _livreRepository.GetByIdAsync(request.Id);
            if (livre == null)
                throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.Id));

            // gerer les emprunts pour eviter de supprimer les livres empruntés ----------- A FAIRE

            await _livreRepository.DeleteAsync(request.Id);
            await _unitOfWork.CompleteAsync();

            return true;
        }

    }
}
