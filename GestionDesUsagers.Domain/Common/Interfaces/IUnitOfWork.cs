
using GestionDesUsagers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Usager> Usagers { get; }
        Task<int> CompleteAsync();
    }
}
