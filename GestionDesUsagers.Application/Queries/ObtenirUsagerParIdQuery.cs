using GestionDesUsagers.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Application.Queries
{
    public class ObtenirUsagerParIdQuery : IRequest<UsagerDto>
    {
        public Guid UsagerId { get; set; }

        public ObtenirUsagerParIdQuery(Guid usagerId)
        {
            UsagerId = usagerId;
        }
    }
}
