using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Application.Commands
{
   public class AjouterUsagerCommand : IRequest<Guid>
    {
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public AjouterUsagerCommand(string nom,string prenoms,string courriel,string telephone)
        {
            Nom = nom;
            Prenoms = prenoms;
            Courriel=courriel;
            Telephone = telephone;
        }
    }

}
