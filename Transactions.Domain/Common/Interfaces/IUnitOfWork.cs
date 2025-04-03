using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transactions.Domain.Common.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        //IGenericRepository<Emprunt> Emprunts { get; }
        Task<int> CompleteAsync();
    }
}
