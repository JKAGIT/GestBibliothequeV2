using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transactions.Domain.Entities;

namespace Transactions.Infrastructure.Persistence
{
    public class GestBibliothequeContext : DbContext
    {
        public GestBibliothequeContext(DbContextOptions<GestBibliothequeContext> options) : base(options)
        {
        }

        public DbSet<Emprunt> Emprunt { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
