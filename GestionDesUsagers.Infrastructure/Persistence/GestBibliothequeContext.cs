using GestionDesUsagers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Infrastructure.Persistence
{
    public class GestBibliothequeContext : DbContext
    {
        public GestBibliothequeContext(DbContextOptions<GestBibliothequeContext> options) : base(options)
        {
        }

        public DbSet<Usager> Usager { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}


