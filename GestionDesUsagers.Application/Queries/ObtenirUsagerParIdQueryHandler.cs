using AutoMapper;
using GestionDesUsagers.Application.DTO;
using GestionDesUsagers.Domain;
using GestionDesUsagers.Domain.Repositories;
using GestionDesUsagers.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Application.Queries
{
    public class ObtenirUsagerParIdQueryHandler : IRequestHandler<ObtenirUsagerParIdQuery, UsagerDto>
    {
        private readonly IUsagerRepository _usagerRepository;
        private readonly IMapper _mapper;

        public ObtenirUsagerParIdQueryHandler(IUsagerRepository usagerRepository, IMapper mapper)
        {
            _usagerRepository = usagerRepository;
            _mapper = mapper;
        }
        public async Task<UsagerDto> Handle(ObtenirUsagerParIdQuery request, CancellationToken cancellationToken)
        {
            var usager = await _usagerRepository.GetByIdAsync(request.UsagerId);
            if (usager == null)
                throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.UsagerId));
            return _mapper.Map<UsagerDto>(usager);
        }
    }
}
