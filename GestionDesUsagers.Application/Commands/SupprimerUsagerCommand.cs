using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Application.Commands
{
    public class SupprimerUsagerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public SupprimerUsagerCommand(Guid id)
        {
            Id = id;
        }
    }
}
