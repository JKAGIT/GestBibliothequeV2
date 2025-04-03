﻿using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class ObtenirLivreParIdQueryHandler : IRequestHandler<ObtenirLivreParIdQuery, LivreDto>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IMapper _mapper;

        public ObtenirLivreParIdQueryHandler(ILivreRepository livreRepository,IMapper mapper)
        {
            _livreRepository = livreRepository;
            _mapper = mapper;
        }

        public async Task<LivreDto> Handle(ObtenirLivreParIdQuery request, CancellationToken cancellationToken)
        {
            var livre = await _livreRepository.GetByIdAsync(request.LivreId);

            if (livre == null)
                throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.LivreId));

            return _mapper.Map<LivreDto>(livre);
        }

    }
}
