using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionDesUsagers.Domain.Entities;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Common;

namespace Transactions.Domain.Entities
{
    public class Emprunt : BaseEntity, IAggregateRoot
    {
        [Key]
        public Guid ID { get; set; }

        public Guid? IDReservation { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire.")]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "La date de retour prévue est obligatoire.")]
        public DateTime DateRetourPrevue { get; set; }

        [Required]
        public Guid IDUsager { get; set; }

        [Required]
        public Guid IDLivre { get; set; }
        //public virtual Retours? Retours { get; set; }
        //public virtual Reservations? Reservation { get; set; }

    }
}
