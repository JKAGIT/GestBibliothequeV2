using GestionDesUsagers.Domain;
using GestionDesUsagers.Domain.Common;
using GestionDesUsagers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestBibliothequeContext _context;
        private IGenericRepository<Usager> _usager;

        public UnitOfWork(GestBibliothequeContext context)
        {
            _context = context;
            _usager = new GenericRepository<Usager>(_context);
        }
        public IGenericRepository<Usager> Usagers => _usager;

        public async Task<int> CompleteAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
