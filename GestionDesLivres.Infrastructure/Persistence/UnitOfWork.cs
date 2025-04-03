using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestBibliothequeContext _context;
        private IGenericRepository<Livre> _livre;
        private IGenericRepository<Categorie> _categorie;

        public UnitOfWork(GestBibliothequeContext context)
        {
            _context = context;
            _livre = new GenericRepository<Livre>(_context);
            _categorie = new GenericRepository<Categorie>(_context);
        }
        public IGenericRepository<Livre> Livres => _livre;
        public IGenericRepository<Categorie> Categories => _categorie;

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
