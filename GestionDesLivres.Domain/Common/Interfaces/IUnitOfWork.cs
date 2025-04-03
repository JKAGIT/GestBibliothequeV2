using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Domain.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Livre> Livres { get; }
        IGenericRepository<Categorie> Categories { get; }
        Task<int> CompleteAsync();
    }
}
