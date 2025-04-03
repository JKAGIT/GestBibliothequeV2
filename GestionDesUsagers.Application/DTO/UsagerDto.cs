using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Application.DTO
{
    public class UsagerDto
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
    }
}
